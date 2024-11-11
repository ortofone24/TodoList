using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Queries;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Tests.Handlers
{
    public class GetTasksByDateQueryHandlerTests
    {
        private readonly AppDbContext _context;
        private readonly GetTasksByDateQueryHandler _handler;

        public GetTasksByDateQueryHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _handler = new GetTasksByDateQueryHandler(_context);
        }

        [Fact]
        public async Task Handle_Tasks_On_Specified_Date_Should_Return_Tasks()
        {
            // Arrange
            var date = DateTime.Today;
            var task1 = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Task 1",
                DueDate = date,
                IsCompleted = false
            };
            var task2 = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Task 2",
                DueDate = date,
                IsCompleted = true
            };
            var task3 = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Task 3",
                DueDate = date.AddDays(1),
                IsCompleted = false
            };

            _context.TaskItems.AddRange(task1, task2, task3);
            await _context.SaveChangesAsync();

            var query = new GetTasksByDateQuery(date);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result.Should().Contain(t => t.Id == task1.Id);
            result.Should().Contain(t => t.Id == task2.Id);
            result.Should().NotContain(t => t.Id == task3.Id);

            // Sprawdzenie sortowania
            var sortedResult = result.ToList();
            sortedResult[0].DueDate.Should().BeOnOrBefore(sortedResult[1].DueDate);
        }

        [Fact]
        public async Task Handle_No_Tasks_On_Specified_Date_Should_Return_Empty()
        {
            // Arrange
            var date = DateTime.Today;
            var query = new GetTasksByDateQuery(date);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
