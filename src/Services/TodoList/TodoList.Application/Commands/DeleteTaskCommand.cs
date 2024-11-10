using MediatR;

namespace TodoList.Application.Commands
{
    public class DeleteTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; }

        public DeleteTaskCommand(Guid id)
        {
            Id = id;
        }
    }
}
