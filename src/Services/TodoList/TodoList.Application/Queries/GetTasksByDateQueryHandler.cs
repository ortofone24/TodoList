using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Queries
{
    public class GetTasksByDateQueryHandler : IRequestHandler<GetTasksByDateQuery, IEnumerable<TaskItem>>
    {
        private readonly AppDbContext _context;

        public GetTasksByDateQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> Handle(GetTasksByDateQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.TaskItems
                .Where(t => t.DueDate.Date == request.Date.Date)
                .ToListAsync(cancellationToken);

            return tasks;
        }
    }
}
