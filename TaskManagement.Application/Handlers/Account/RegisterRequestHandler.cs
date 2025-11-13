using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Application.Dtos;
using TaskManagement.Application.Extensions;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Requests;
using TaskManagement.Application.Validators.Account;
using TaskManagement.Domain.Interfaces;

namespace TaskManagement.Application.Handlers.Account
{
	
	public class RegisterRequestHandler : IRequestHandler<RegisterRequest, Result<NoData>>
	{
		private readonly IUserRepository _userRepository;
		private  readonly IUsernameNormalizer _normalizer;

        public RegisterRequestHandler(IUserRepository userRepository, IUsernameNormalizer normalizer)
        {
            _userRepository = userRepository;
            _normalizer = normalizer;
        }

        public async Task<Result<NoData>> Handle(RegisterRequest request, CancellationToken cancellationToken)
		{
			var validator = new RegisterRequestValidator(_normalizer,_userRepository);
			var validationResult = await validator.ValidateAsync(request);
			if (validationResult != null) {
				if (validationResult.IsValid)
				{
					var user = await _userRepository.GetByFilterAsync(x => x.NormalizedUsername == request.Username.ToLowerInvariant());
					if (user != null)
					{
						return new Result<NoData>(null, false, "Bu kullanıcı adı zaten alınmış", null);
					}
					else
					{
						var rowCount = await _userRepository.CreateAsync(request.ToMap(_normalizer));
						if (rowCount > 0)
						{
							return new Result<NoData>(new NoData(), true, null, null);
						}
						else
						{
							return new Result<NoData>(new NoData(), false, "Kullanıcı kaydı başarısız", null);
						}
					}	
				}
				else
				{
					var errorList = validationResult.Errors.ToMap();
					return new Result<NoData>(null, false, null, errorList);
				}
			}
			else
			{
				return new Result<NoData>(null, false, "Beklenmeyen bir hata oluştu", null);
			}

		}
	}
}
