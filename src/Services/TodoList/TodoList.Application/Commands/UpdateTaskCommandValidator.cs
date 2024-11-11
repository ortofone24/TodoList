using FluentValidation;

namespace TodoList.Application.Commands
{
    public class UpdateTaskCommandValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Identyfikator zadania jest wymagany.");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tytuł jest wymagany.")
                .MaximumLength(100).WithMessage("Tytuł może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Termin jest wymagany.");
        }
    }
}
