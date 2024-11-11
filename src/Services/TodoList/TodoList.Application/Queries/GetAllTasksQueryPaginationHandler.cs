using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Queries
{
    public class GetAllTasksQueryPaginationHandler : IRequestHandler<GetAllTasksQueryPagination, PaginatedResult<TaskItem>>
    {
        private readonly AppDbContext _context;

        public GetAllTasksQueryPaginationHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PaginatedResult<TaskItem>> Handle(GetAllTasksQueryPagination request, CancellationToken cancellationToken)
        {
            var query = _context.TaskItems.AsQueryable();

            query = query.OrderBy(t => t.DueDate);

            var totalCount = await query.CountAsync(cancellationToken);

            var tasks = await query
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<TaskItem>
            {
                Items = tasks,
                TotalCount = totalCount,
                PageNumber = request.PageNumber,
                PageSize = request.PageSize
            };
        }
    }
}
