using System.Net.Http.Json;
using TodoList.Web.Models;

namespace TodoList.Web.Services
{
    public class TaskService : ITaskService
    {
        private readonly HttpClient _httpClient;

        public TaskService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PaginatedResult<TaskItem>> GetTaskItemsPagination(int pageNumber = 1, int pageSize = 10)
        {
            var response = await _httpClient.GetFromJsonAsync<PaginatedResult<TaskItem>>($"api/Task/pagination?pageNumber={pageNumber}&pageSize={pageSize}");
            return response;
        }
 
        public async Task CreateTask(CreateTaskCommand command)
        {
            var response = await _httpClient.PostAsJsonAsync("api/Task", command);
            response.EnsureSuccessStatusCode();

        }

        public async Task DeleteTask(Guid id)
        {
            var response = await _httpClient.DeleteAsync($"api/Task/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<IEnumerable<TaskItem>> GetAllTasks()
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TaskItem>>("api/Task/all");
            return response;
        }

        public async Task<TaskItem> GetTaskById(Guid id)
        {
            var response = await _httpClient.GetFromJsonAsync<TaskItem>($"api/Task/{id}");
            return response;
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByDate(DateTime date)
        {
            var response = await _httpClient.GetFromJsonAsync<IEnumerable<TaskItem>>($"api/Task?date={date.ToString("yyyy-MM-dd")}");
            return response;
        }

        public async Task UpdateTask(UpdateTaskCommand command)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/Task/{command.Id}", command);
            response.EnsureSuccessStatusCode();
        }
    }
}
