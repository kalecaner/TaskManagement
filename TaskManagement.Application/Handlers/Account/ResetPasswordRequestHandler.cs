using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Handlers.Account
{
    public class ResetPasswordRequestHandler : IRequestHandler<ResetPasswordRequest, Result<NoData>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordResetTokenRepository _passwordResetTokenRepository;

        public ResetPasswordRequestHandler(IUserRepository userRepository, IPasswordResetTokenRepository passwordResetTokenRepository)
        {
            _userRepository = userRepository;
            _passwordResetTokenRepository = passwordResetTokenRepository;
        }

        public async Task<Result<NoData>> Handle(ResetPasswordRequest request, CancellationToken cancellationToken)
        {
            var token= await _passwordResetTokenRepository.GetByTokenAsync(request.Token);
            if (token == null || token.ExpireDate < DateTime.UtcNow)
            {
               return new Result<NoData>(new NoData(), false, "token invalid", null);

            }
            var user = await _userRepository.GetByFilterAsync(x=>x.Id==token.AppUserId);
            if (user == null)
            {
                return new Result<NoData>(new NoData(), false, "user not found", null);
            }
            user.Password = request.NewPassword;
            token.IsUsed = true;
            await _userRepository.UpdateAsync(user);
            await _passwordResetTokenRepository.UpdateAsync(token);
            return new Result<NoData>(new NoData(), true, null, null);
        }
    }
}
