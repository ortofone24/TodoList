using MediatR;

namespace TodoList.Application.Commands
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; }
        public DateTime DueDate { get; set; }
    }
}
