using MediatR;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Commands
{
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly AppDbContext _context;

        public CreateTaskCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                DueDate = request.DueDate,
                IsCompleted = false
            };

            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync(cancellationToken);

            return taskItem.Id;
        }
    }
}
