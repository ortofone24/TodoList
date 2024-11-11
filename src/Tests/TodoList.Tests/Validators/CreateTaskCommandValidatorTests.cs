using FluentValidation.TestHelper;
using TodoList.Application.Commands;

namespace TodoList.Tests.Validators
{
    public class CreateTaskCommandValidatorTests
    {
        private readonly CreateTaskCommandValidator _validator;

        public CreateTaskCommandValidatorTests()
        {
            _validator = new CreateTaskCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Title_Is_Null()
        {
            var command = new CreateTaskCommand
            {
                Title = null,
                DueDate = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Tytuł jest wymagany.");
        }

        [Fact]
        public void Should_Have_Error_When_Title_Exceeds_MaxLength()
        {
            var command = new CreateTaskCommand
            {
                Title = new string('a', 101),
                DueDate = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Title)
                  .WithErrorMessage("Tytuł może mieć maksymalnie 100 znaków.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Title_Is_Valid()
        {
            var command = new CreateTaskCommand
            {
                Title = "Valid Title",
                DueDate = DateTime.Today.AddDays(1)
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Title);
        }

        [Fact]
        public void Should_Not_Have_Error_When_DueDate_Is_In_Past()
        {
            var command = new CreateTaskCommand
            {
                Title = "Valid Title",
                DueDate = DateTime.Today.AddDays(-1)
            };

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.DueDate);
                  
        }

        [Fact]
        public void Should_Not_Have_Error_When_DueDate_Is_Today_Or_Future()
        {
            var commandToday = new CreateTaskCommand
            {
                Title = "Valid Title",
                DueDate = DateTime.Today
            };

            var commandFuture = new CreateTaskCommand
            {
                Title = "Valid Title",
                DueDate = DateTime.Today.AddDays(1)
            };

            var resultToday = _validator.TestValidate(commandToday);
            var resultFuture = _validator.TestValidate(commandFuture);

            resultToday.ShouldNotHaveValidationErrorFor(x => x.DueDate);
            resultFuture.ShouldNotHaveValidationErrorFor(x => x.DueDate);
        }
    }
}
