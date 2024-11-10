using MediatR;
using TodoList.Application.Exceptions;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Commands
{
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, Unit>
    {
        private readonly AppDbContext _context;

        public UpdateTaskCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);

            if (taskItem == null)
            {
                throw new TaskNotFoundException($"Task with ID {request.Id} not found.");
            }

            taskItem.Title = request.Title;
            taskItem.DueDate = request.DueDate;
            taskItem.IsCompleted = request.IsCompleted;

            _context.TaskItems.Update(taskItem);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
