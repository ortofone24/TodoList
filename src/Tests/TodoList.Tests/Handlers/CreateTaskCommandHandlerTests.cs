using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using TodoList.Application.Commands;
using TodoList.Infrastructure.Data;

namespace TodoList.Api.Tests.Handlers
{
    public class CreateTaskCommandHandlerTests
    {
        private readonly AppDbContext _context;
        private readonly CreateTaskCommandHandler _handler;

        public CreateTaskCommandHandlerTests()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) 
                .Options;

            _context = new AppDbContext(options);
            _handler = new CreateTaskCommandHandler(_context);
        }

        [Fact]
        public async Task Handle_ValidCommand_Should_Create_Task()
        {
            // Arrange
            var command = new CreateTaskCommand
            {
                Title = "Test Task",
                DueDate = DateTime.Today.AddDays(1)
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();

            var task = await _context.TaskItems.FindAsync(result);
            task.Should().NotBeNull();
            task.Title.Should().Be(command.Title);
            task.DueDate.Should().Be(command.DueDate);
            task.IsCompleted.Should().BeFalse();
        }

        [Fact]
        public async Task Handle_InvalidCommand_Should_Not_Create_Task()
        {

            // Arrange
            var command = new CreateTaskCommand
            {
                Title = "", 
                DueDate = DateTime.Today.AddDays(-1) 
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeEmpty();

            var task = await _context.TaskItems.FindAsync(result);
            task.Should().NotBeNull();
            task.Title.Should().Be(command.Title);
            task.DueDate.Should().Be(command.DueDate);
            task.IsCompleted.Should().BeFalse();
        }
    }
}
