using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Commands;
using TodoList.Application.Dtos;
using TodoList.Application.Queries;
using TodoList.Domain.Models;

namespace TodoList.Api.Controllers
{
    public class TaskController : ApiController
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("pagination")]
        public async Task<ActionResult<PaginatedResult<TaskItem>>> GetTaskItemsPagination([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllTasksQueryPagination { PageNumber = pageNumber, PageSize = pageSize };
            var paginatedTasks = await _mediator.Send(query);
            return Ok(paginatedTasks);
        }

        // GET: api/Task/all
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            var query = new GetAllTasksQuery();
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }


        // GET: api/Task?date=2021-12-01
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTasks([FromQuery] DateTime date)
        {
            var query = new GetTasksByDateQuery(date);
            var tasks = await _mediator.Send(query);
            return Ok(tasks);
        }

        // GET: api/Task/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(Guid id)
        {
            var query = new GetTaskByIdQuery(id);
            var task = await _mediator.Send(query);
            if (task == null)
            {
                return NotFound();
            }
            return Ok(task);
        }

        // POST: api/Task
        [HttpPost]
        public async Task<CreateTaskResponse> PostTaskItem([FromBody] CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return new CreateTaskResponse { Id = taskId };
        }

        // PUT: api/Task/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await _mediator.Send(command);
            return NoContent();
        }

        // DELETE: api/Task/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var command = new DeleteTaskCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}
