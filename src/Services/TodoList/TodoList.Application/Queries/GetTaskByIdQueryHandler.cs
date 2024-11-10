using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Application.Queries
{
    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskItem>
    {
        private readonly AppDbContext _context;

        public GetTaskByIdQueryHandler(AppDbContext context)
        {
            _context = context;
        }

        public async Task<TaskItem> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskItem = await _context.TaskItems.FindAsync(new object[] { request.Id }, cancellationToken);
            return taskItem;
        }
    }
}
