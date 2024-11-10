using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Controllers
{
    public class TaskItemsController : ApiController
    {
        private readonly AppDbContext _context;

        public TaskItemsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/TaskItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetTaskItems()
        {
            return await _context.TaskItems.ToListAsync();
        }

        // GET: api/TaskItems/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TaskItem>> GetTaskItem(Guid id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);

            if (taskItem == null)
            {
                return NotFound();
            }

            return taskItem;
        }

        // POST: api/TaskItems
        [HttpPost]
        public async Task<ActionResult<TaskItem>> PostTaskItem(TaskItem taskItem)
        {
            taskItem.Id = Guid.NewGuid(); // Ensure a new GUID is assigned
            _context.TaskItems.Add(taskItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTaskItem), new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/TaskItems/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTaskItem(Guid id, TaskItem taskItem)
        {
            if (id != taskItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(taskItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskItemExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // DELETE: api/TaskItems/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTaskItem(Guid id)
        {
            var taskItem = await _context.TaskItems.FindAsync(id);
            if (taskItem == null)
            {
                return NotFound();
            }

            _context.TaskItems.Remove(taskItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskItemExists(Guid id)
        {
            return _context.TaskItems.Any(e => e.Id == id);
        }
    }
}
