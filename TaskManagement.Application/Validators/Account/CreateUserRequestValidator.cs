using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Validators.Account
{
    public class CreateUserRequestValidator:AbstractValidator<CreateUserRequest>
    { 

        public CreateUserRequestValidator(IUserRepository _userRepository)
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(RuleFor => RuleFor.ConfirmPassword)
                .Equal(RuleFor => RuleFor.Password)
                .WithMessage("Password and Confirm Password must match");
            RuleFor(x => x.RoleId).GreaterThan(0).WithMessage("RoleId must be greater than 0");
            RuleFor(x => x.Username).NotEmpty().MaximumLength(50).MustAsync( async (Username,ct)=>
            {

                return await BeUniqueUsername(Username, _userRepository);
            }).WithMessage("Bu kullanıcı adı sistemde mevcut.");

        

    }
        private async Task<bool> BeUniqueUsername(string userName, IUserRepository _userRepository)
        {
            var normalized = userName.ToUpperInvariant().Trim();
             bool exists=await _userRepository.ExistsByNormalizedUserNameAsync(normalized);
            return !exists;
        }

    }
}
