using FluentValidation;

namespace TodoList.Application.Queries
{
    public class GetTaskByIdQueryValidator : AbstractValidator<GetTaskByIdQuery>
    {
        public GetTaskByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identyfikator zadania jest wymagany.")
                .Must(id => id != Guid.Empty).WithMessage("Identyfikator zadania nie może być pusty.");
        }
    }
}
