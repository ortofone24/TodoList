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

                // Czekaj przez określony czas przed kolejnym sprawdzeniem
                //await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
                await Task.Delay(TimeSpan.FromSeconds(30), stoppingToken);
            }
        }

        private async Task CheckAndSendNotifications(CancellationToken cancellationToken)
        {
            using (var scope = _services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                // Znajdź zadania, które są do wykonania w ciągu najbliższych 24 godzin
                var upcomingTasks = await context.TaskItems
                    .Where(t => !t.IsCompleted && t.DueDate <= DateTime.UtcNow.AddHours(48) && t.DueDate > DateTime.UtcNow)
                    .ToListAsync(cancellationToken);

                foreach (var task in upcomingTasks)
                {
                    // Wysyłanie powiadomienia do wszystkich podłączonych klientów
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
