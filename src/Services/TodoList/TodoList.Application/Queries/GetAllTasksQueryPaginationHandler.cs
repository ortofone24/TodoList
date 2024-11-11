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
            var totalCount = await _context.TaskItems.CountAsync(cancellationToken);
            var tasks = await _context.TaskItems
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
