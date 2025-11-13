using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Validators.Account
{
	public class LoginRequestValidator:AbstractValidator<LoginRequest>
	{
		public LoginRequestValidator()
		{
		RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.");
		RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
		RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 characters long.");
		}
	}
	
}
