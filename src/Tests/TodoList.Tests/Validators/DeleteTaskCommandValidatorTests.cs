using FluentValidation.TestHelper;
using TodoList.Application.Commands;

namespace TodoList.Tests.Validators
{
    public class DeleteTaskCommandValidatorTests
    {
        private readonly DeleteTaskCommandValidator _validator;

        public DeleteTaskCommandValidatorTests()
        {
            _validator = new DeleteTaskCommandValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Id_Is_Empty()
        {
            var command = new DeleteTaskCommand(Guid.Empty);

            var result = _validator.TestValidate(command);
            result.ShouldHaveValidationErrorFor(x => x.Id)
                  .WithErrorMessage("Identyfikator zadania nie może być pusty.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Id_Is_Valid()
        {
            var command = new DeleteTaskCommand(Guid.NewGuid());

            var result = _validator.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(x => x.Id);
        }
    }
}
