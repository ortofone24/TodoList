using MediatR;
using TodoList.Application.Exceptions;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Commands
{
    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, Unit>
    {
        private readonly AppDbContext _context;

        public DeleteTaskCommandHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);

            if (taskItem == null)
            {
                throw new TaskNotFoundException($"Task with ID {request.Id} not found.");
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
