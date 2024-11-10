using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Queries
{
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskItem>>
    {
        private readonly AppDbContext _context;

        public GetAllTasksQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TaskItem>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var tasks = await _context.TaskItems.ToListAsync(cancellationToken);
            return tasks;
        }
    }
}
