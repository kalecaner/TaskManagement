using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Results;

namespace TaskManagement.Application.Validators.AppTask
{
    public class AppTaskCreateRequestValidator: AbstractValidator<AppTaskCreateRequest>
    {
        public AppTaskCreateRequestValidator()
        {
            this.RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            this.RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");

        }
    }
}
