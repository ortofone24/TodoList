using MediatR;

namespace TodoList.Application.Commands
{
    public class UpdateTaskCommand : IRequest<Unit>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
