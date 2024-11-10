using MediatR;
using TodoList.Domain.Models;

namespace TodoList.Application.Queries
{
    public class GetAllTasksQuery : IRequest<IEnumerable<TaskItem>>
    {
    }
}
