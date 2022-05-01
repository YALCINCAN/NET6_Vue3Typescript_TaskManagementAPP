using Business.Handlers.Tasks.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Handlers.Tasks.Validations
{
    public class CreateTaskValidator: AbstractValidator<CreateTaskCommand>
    {
        public CreateTaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(5); ;
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MinimumLength(5);
            RuleFor(x => x.StringDeadline).NotEmpty().WithMessage("Deadline is required");
            RuleFor(x => x.UserIds).NotEmpty().WithMessage("At least one user is required");
        }
    }

    public class UpdateTaskValidator : AbstractValidator<UpdateTaskCommand>
    {
        public UpdateTaskValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MinimumLength(5);
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required").MinimumLength(5);
            RuleFor(x => x.StringDeadline).NotEmpty().WithMessage("Deadline is required");
            RuleFor(x => x.TaskStatusId).NotEmpty().WithMessage("Task Status is required");
            RuleFor(x => x.UserIds).NotEmpty().WithMessage("At least one user is required");
        }
    }
}
