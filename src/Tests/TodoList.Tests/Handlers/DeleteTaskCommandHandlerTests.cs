using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Commands;
using TodoList.Application.Exceptions;
using TodoList.Domain.Models;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Tests.Handlers
{
    public class DeleteTaskCommandHandlerTests
    {
        private readonly AppDbContext _context;
        private readonly DeleteTaskCommandHandler _handler;

        public DeleteTaskCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new AppDbContext(options);
            _handler = new DeleteTaskCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_ExistingTask_Should_Delete_Task()
        {
            // Arrange
            var task = new TaskItem
            {
                Id = Guid.NewGuid(),
                Title = "Task to Delete",
                DueDate = DateTime.Today.AddDays(1),
                IsCompleted = false
            };

            _context.TaskItems.Add(task);
            await _context.SaveChangesAsync();

            var command = new DeleteTaskCommand(task.Id);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert

            var deletedTask = await _context.TaskItems.FindAsync(task.Id);
            deletedTask.Should().BeNull();
        }

        [Fact]
        public async Task Handle_NonExistingTask_Should_Return_False()
        {
            // Arrange
            var command = new DeleteTaskCommand(Guid.NewGuid());


            // Act & Assert
            await FluentActions
                .Invoking(() => _handler.Handle(command, CancellationToken.None))
                .Should()
                .ThrowAsync<TaskNotFoundException>("because the task does not exist");
        }
    }
}
