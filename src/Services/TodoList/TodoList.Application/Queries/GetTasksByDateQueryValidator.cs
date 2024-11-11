using FluentValidation;

namespace TodoList.Application.Queries
{
    public class GetTasksByDateQueryValidator : AbstractValidator<GetTasksByDateQuery>
    {
        public GetTasksByDateQueryValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Data jest wymagana.");
        }
    }
}
