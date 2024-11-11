using FluentValidation;

namespace TodoList.Application.Commands
{
    public class DeleteTaskCommandValidator : AbstractValidator<DeleteTaskCommand>
    {
        public DeleteTaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identyfikator zadania jest wymagany.")
                .Must(id => id != Guid.Empty).WithMessage("Identyfikator zadania nie może być pusty.");
        }
    }
}
