using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Validators.AppTask
{
    public class AppTaskUpdateRequestValidator : AbstractValidator<AppTaskUpdateRequest>
    {
        public AppTaskUpdateRequestValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            this.RuleFor(x => x.PriorityId).NotEmpty().WithMessage("Priority is required");
        }
    }
}
