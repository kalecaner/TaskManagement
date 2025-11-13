using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Validators.Account
{
    public class SendPasswordResetRequestValidator:AbstractValidator<SendPasswordResetMailRequest>
    {
        public SendPasswordResetRequestValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş olamaz.");
                
        }
    
    }
}
