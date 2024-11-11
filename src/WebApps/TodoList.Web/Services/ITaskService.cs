using TodoList.Web.Models;

namespace TodoList.Web.Services
{
    public interface ITaskService
    {
        Task<PaginatedResult<TaskItem>> GetTaskItemsPagination(int pageNumber = 1, int pageSize = 10);
        Task<IEnumerable<TaskItem>> GetAllTasks(); 
        Task<IEnumerable<TaskItem>> GetTasksByDate(DateTime date);
        Task<TaskItem> GetTaskById(Guid id);
        Task CreateTask(CreateTaskCommand command);
        Task UpdateTask(UpdateTaskCommand command);
        Task DeleteTask(Guid id);
    }
}
