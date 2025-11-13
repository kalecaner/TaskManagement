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
    public class MemberPageListRequestHandler : IRequestHandler<MemberPageListRequest, PagedResult<MemberDto>>
    {
        private readonly IUserRepository _userRepository;

        public MemberPageListRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<PagedResult<MemberDto>> Handle(MemberPageListRequest request, CancellationToken cancellationToken)
        {
            var members = await _userRepository.GetAllAsyncByPage(request.activePage, (int)RoleType.Member, request.SearchString, 5);
            var memberDtos = members.Data.Select(u => new MemberDto(u.Id, u.Name, u.Surname, u.Username, RoleType.Member)).ToList();


            return new PagedResult<MemberDto>(memberDtos, request.activePage, members.PageSize, members.TotalPages);
        }
    }
}
