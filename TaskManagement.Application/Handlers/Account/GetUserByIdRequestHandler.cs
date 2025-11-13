using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;

namespace TaskManagement.Application.Handlers.Account
{
    public class GetUserByIdRequestHandler : IRequestHandler<GetUserByIdRequest, Result<UpdateDto?>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<UpdateDto?>> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByFilterAsync(x=>x.Id==request.UserId);
            if (user == null)
            {
                return  new Result<UpdateDto?>( null, false, "Kullanıcı bulunamadı", null);
            }
            else
            {
                return new Result<UpdateDto?>(new UpdateDto(user.Id, user.Name, user.Surname, user.Username, (RoleType)user.AppRoleId, user.Email), true, null, null);
            }
        }
    }
}
