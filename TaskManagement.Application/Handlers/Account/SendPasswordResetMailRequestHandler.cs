using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Domain.Entities;

namespace TaskManagement.Application.Handlers.Account
{
    public class SendPasswordResetMailRequestHandler : IRequestHandler<SendPasswordResetMailRequest, Result<NoData>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;
        private readonly IPasswordResetTokenGenerator _passwordResetTokenGenerator;
        private readonly IEmailSender _emailSender;
        private readonly IResetLinkBuilder _resetLinkBuilder;

        public SendPasswordResetMailRequestHandler(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository, IPasswordResetTokenGenerator passwordResetTokenGenerator, IEmailSender emailSender, IResetLinkBuilder resetLinkBuilder)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
            _passwordResetTokenGenerator = passwordResetTokenGenerator;
            _emailSender = emailSender;
            _resetLinkBuilder = resetLinkBuilder;
        }

        public async Task<Result<NoData>> Handle(SendPasswordResetMailRequest request, CancellationToken cancellationToken)
        {
            var  normalizedUsername = request.Username.Trim().ToLowerInvariant();
            var  user = await  _userRepository.GetByFilterAsync(x => x.NormalizedUsername == normalizedUsername);
            if (user == null || string.IsNullOrWhiteSpace(user.Email))
            {
                return new Result<NoData>(new NoData(), false, "username invalid or  email dont found", null);
            }
            var tokenvalue = _passwordResetTokenGenerator.Generate();
            var passwordResetToken = new PasswordResetToken
            { 
                AppUserId = user.Id,
                Token = tokenvalue,
                ExpireDate = DateTime.UtcNow.AddHours(1)
            };
            await _passwordResetTokenRepository.CreateAsync(passwordResetToken);
             var resetlink=_resetLinkBuilder.Build(tokenvalue);
            var emailbody = $"<p>To reset your password, please click the link below:</p><a href='{resetlink}'>Reset Password</a>";
            var subject = "Password Reset Request"; 
            await _emailSender.SendAsync(user.Email,subject,emailbody);


            return new Result<NoData>(new NoData(), true, "if there is User, Password reset mail sent", null);
        }
    }
}
