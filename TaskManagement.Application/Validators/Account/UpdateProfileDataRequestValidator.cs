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
    public class UpdateProfileDataRequestValidator: AbstractValidator<UpdateProfileDataRequest>
    {
        public UpdateProfileDataRequestValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname is required");         
           
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("A valid email is required");
            RuleFor(RuleFor => RuleFor.ConfirmPassword)
                .Equal(RuleFor => RuleFor.Password)
                .WithMessage("Password and Confirm Password must match");           

        }

        


    }
}
