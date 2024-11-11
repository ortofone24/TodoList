using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using TodoList.Api.Hubs;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Services
{
    public class NotificationBackgroundService : BackgroundService
    {

        private readonly IServiceProvider _services;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationBackgroundService(IServiceProvider services, IHubContext<NotificationHub> hubContext)
        {
            _services = services;
            _hubContext = hubContext;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckAndSendNotifications(stoppingToken);

                // Wait for a certain period of time before checking again
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task CheckAndSendNotifications(CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Find tasks that need to be done in the next 48 hours
                var upcomingTasks = await context.TaskItems
                    .Where(t => !t.IsCompleted && t.DueDate <= DateTime.UtcNow.AddHours(48) && t.DueDate > DateTime.UtcNow)
                    .ToListAsync(cancellationToken);

                foreach (var task in upcomingTasks)
                {
                    // Sending notification to all connected clients
                    await _hubContext.Clients.All.SendAsync("ReceiveNotification", new
                    {
                        TaskId = task.Id,
                        Title = task.Title,
                        DueDate = task.DueDate
                    }, cancellationToken);
                }
            }

        }
    }
}
