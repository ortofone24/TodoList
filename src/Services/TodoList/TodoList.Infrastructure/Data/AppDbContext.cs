using Microsoft.EntityFrameworkCore;
using TodoList.Domain.Models;

namespace TodoList.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<TaskItem> TaskItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
