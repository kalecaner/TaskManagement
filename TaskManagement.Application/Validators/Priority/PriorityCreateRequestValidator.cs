using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Validators.Priority
{
	public class PriorityCreateRequestValidator: AbstractValidator<PriorityCreateRequest>
	{
		public PriorityCreateRequestValidator()
		{
			RuleFor(x => x.Definition)
				.NotEmpty().WithMessage("Definition is required.");
		}
	}
}
