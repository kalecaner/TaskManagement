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
    public class DeleteUserRequestHandler : IRequestHandler<DeleteUserRequest, Result<NoData>>
    { private readonly IUserRepository _userRepository;

        public DeleteUserRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<NoData>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
        {

            var result = await _userRepository.GetByFilterAsync(x => x.Id == request.UserId);
            if (result ==null)
            {
                return new Result<NoData>(null, false, "User not found", null);
            }
            else
            {               
                await _userRepository.DeleteByIdAsync(result);
                return new Result<NoData>(null, true, "User deleted successfully", null);
            }
        }
    }
}
