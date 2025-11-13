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
    public class CreateUserRequestHandler : IRequestHandler<CreateUserRequest, Result<NoData>>
    { private readonly IUserRepository _userRepository;
        private readonly IUsernameNormalizer _normalizer;

        public CreateUserRequestHandler(IUserRepository userRepository, IUsernameNormalizer normalizer)
        {
            _userRepository = userRepository;
            _normalizer = normalizer;
        }

        public async Task<Result<NoData>> Handle(CreateUserRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateUserRequestValidator(_userRepository);
            var validationResult = await  validator.ValidateAsync(request);
            if(validationResult.IsValid)
            {
                var result = await _userRepository.CreateAsync(request.ToMap(_normalizer));
                if(result>0)
                {
                    return new Result<NoData>(new NoData(), true, null, null);
                }
                else
                {
                    return new Result<NoData>(new NoData(), false, "Kayıt Eklenemedi", null);
                }
            }
            else
            {
                return new Result<NoData>(new NoData(), false, null, validationResult.Errors.ToMap());
            }
        }
    }
}
