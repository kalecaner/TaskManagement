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

namespace TaskManagement.Application.Handlers.Account
{
    public class UpdateProfileDataRequestHandler : IRequestHandler<UpdateProfileDataRequest, Result<NoData>>
    {
        private readonly IUserRepository _userRepository;

        public UpdateProfileDataRequestHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<NoData>> Handle(UpdateProfileDataRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProfileDataRequestValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid)
            {
                var data = await _userRepository.GetByFilterAsync(u => u.Id == request.UserId);
                if (data == null)
                {
                    return new Result<NoData>(new NoData(), false, "Kullanıcı Bulunamadı", null);
                }
                data.Surname = request.Surname;
                data.Name = request.Name;
                data.Email = request.Email;
                if (!string.IsNullOrWhiteSpace(request.Password))
                {
                    data.Password = request.Password;
                }
                var result = await _userRepository.SaveChangeAsync();

                if (result > 0)
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
