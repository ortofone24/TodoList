using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Infrastructure.Extensions
{
    internal class InitialData
    {
        public static void SeedData(AppDbContext context)
        {
            if (!context.TaskItems.Any())
            {
                var random = new Random();
                var taskItems = new List<TaskItem>();

                for (int i = 0; i < 10; i++)
                {
                    taskItems.Add(new TaskItem
                    {
                        Id = Guid.NewGuid(),
                        Title = $"Zadanie {i + 1}",
                        DueDate = DateTime.Now.AddDays(random.Next(1, 30)),
                        IsCompleted = random.Next(0, 2) == 1
                    });
                }

                context.TaskItems.AddRange(taskItems);
                context.SaveChanges();
            }
        }
    }
}
