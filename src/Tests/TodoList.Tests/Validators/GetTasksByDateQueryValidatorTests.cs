using FluentValidation.TestHelper;
using TodoList.Application.Queries;

namespace TodoList.Tests.Validators
{
    public class GetTasksByDateQueryValidatorTests
    {
        private readonly GetTasksByDateQueryValidator _validator;

        public GetTasksByDateQueryValidatorTests()
        {
            _validator = new GetTasksByDateQueryValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Date_Is_Default()
        {
            var query = new GetTasksByDateQuery(default(DateTime));

            var result = _validator.TestValidate(query);
            result.ShouldHaveValidationErrorFor(x => x.Date)
                  .WithErrorMessage("Data jest wymagana.");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Date_Is_In_Future()
        {
            var query = new GetTasksByDateQuery(DateTime.Today.AddDays(1));

            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Date_Is_Today()
        {
            var query = new GetTasksByDateQuery(DateTime.Today);

            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
        }

        [Fact]
        public void Should_Not_Have_Error_When_Date_Is_In_Past()
        {
            var query = new GetTasksByDateQuery(DateTime.Today.AddDays(-1));

            var result = _validator.TestValidate(query);
            result.ShouldNotHaveValidationErrorFor(x => x.Date);
        }
    }
}
