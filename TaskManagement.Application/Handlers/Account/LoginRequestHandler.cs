using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Enums;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Validators.Account;

namespace TaskManagement.Application.Handlers.Account
{
	public class LoginRequestHandler : IRequestHandler<LoginRequest, Result<LoginResponseDto?>>
	{


		private readonly IUserRepository _userRepository;

		public LoginRequestHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<Result<LoginResponseDto?>> Handle(LoginRequest request, CancellationToken cancellationToken)
		{
			var validator = new LoginRequestValidator();	
			var validationResult =await  validator.ValidateAsync(request);
			if (validationResult.IsValid)
			{
				var User = await _userRepository.GetByFilterAsNoTrackingAsync(x => x.Username == request.Username && x.Password == request.Password);
				

				if (User != null)
				{
					var type = (RoleType)User.AppRoleId;
					return new Result<LoginResponseDto?>(new LoginResponseDto(User.Name, User.Surname,User.Id, type), true, null, null);
				}
				else
				{
					return new Result<LoginResponseDto?>(null, false, "Kullanıcı adı veya şifre hatalı", null);
				}
			}
			else
			{


				var errorList = validationResult.Errors.ToMap();
				
				return new Result<LoginResponseDto?>(null, false, null, errorList);

			}
		}
	}
}
