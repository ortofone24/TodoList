using FluentValidation;

namespace TodoList.Application.Commands
{
    public class CreateTaskCommandValidator : AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Tytuł jest wymagany.")
                .MaximumLength(100).WithMessage("Tytuł może mieć maksymalnie 100 znaków.");

            RuleFor(x => x.DueDate)
                .NotEmpty().WithMessage("Termin jest wymagany.");
        }
    }
}
