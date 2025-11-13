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
    public class UpdateUserRequestHandler : IRequestHandler<UpdateUserRequest, Result<NoData>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<NoData>> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var  UserUpdateResult= await _userRepository.GetByFilterAsync(x => x.Id == request.Id);
            if (UserUpdateResult == null)
            {
                return new Result<NoData>(null, false, "User  didnt find", null);
               
            }
            else
            {
                UserUpdateResult.Email = request.Email ?? UserUpdateResult.Email;
                UserUpdateResult.Name = request.Name ?? UserUpdateResult.Name;
                UserUpdateResult.Surname = request.Surname ?? UserUpdateResult.Surname;
                UserUpdateResult.AppRoleId = request.RoleId != 0 ? request.RoleId : UserUpdateResult.AppRoleId ;
                // await _userRepository.UpdateAsync(UserUpdateResult);
                var updateResult = await _userRepository.SaveChangeAsync();
                if (updateResult > 0)
                {
                    return new Result<NoData>(new NoData(), true, null, null);
                }
                else
                {
                    return new Result<NoData>(new NoData(), false, "User couldnt update", null);
                }
            }
        }
    }
}
