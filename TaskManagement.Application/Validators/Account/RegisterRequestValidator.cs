using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Validators.Account
{
	public class RegisterRequestValidator:AbstractValidator<RegisterRequest>
	{
		public RegisterRequestValidator(IUsernameNormalizer normalizer,IUserRepository repository)
		{
			RuleFor(x => x.Username).NotEmpty().MaximumLength(50).WithMessage("Username is required.");
			RuleFor(x=>x.Username).MustAsync(async(username,ct)=>
			{
				 var normalized=normalizer.Normalize(username);
				return !await repository.ExistsByNormalizedUserNameAsync(normalized);
            }).WithMessage("Username already exists.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
			RuleFor(x => x.Password).MinimumLength(5).WithMessage("Password must be at least 5 characters long.");
			RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required.");
			RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required.");
			RuleFor(x => x.ConfirmPassword)
				.NotEmpty().WithMessage("Confirm Password is required.")
				.Equal(x => x.Password).WithMessage("Confirm Password must match Password.");

		}
	}
}
