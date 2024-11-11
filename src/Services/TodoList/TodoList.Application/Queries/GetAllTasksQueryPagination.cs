using MediatR;
using TodoList.Domain.Models;

namespace TodoList.Application.Queries
{
    public class GetAllTasksQueryPagination : IRequest<PaginatedResult<TaskItem>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class PaginatedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
