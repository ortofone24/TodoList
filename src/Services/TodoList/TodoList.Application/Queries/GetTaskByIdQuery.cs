using MediatR;
using TodoList.Domain.Models;

namespace TodoList.Application.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskItem>
    {
        public Guid Id { get; }

        public GetTaskByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}
