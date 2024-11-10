using MediatR;
using TodoList.Domain.Models;

namespace TodoList.Application.Queries
{
    public class GetTasksByDateQuery : IRequest<IEnumerable<TaskItem>>
    {
        public DateTime Date { get; set; }

        public GetTasksByDateQuery(DateTime date)
        {
            Date = date;
        }
    }
}
