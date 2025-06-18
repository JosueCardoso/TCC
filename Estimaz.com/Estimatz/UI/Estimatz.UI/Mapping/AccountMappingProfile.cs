using AutoMapper;
using Estimatz.Commands.Common;
using Estimatz.Commands.Account.ConfirmRecoverPassword;
using Estimatz.Commands.Account.Register;
using Estimatz.UI.Models;
using Estimatz.Queries.Account.SignIn;

namespace Estimatz.UI.Mapping
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateAccountModel, RegisterNewUserCommand>()
                .ForMember(dest => dest.Username, opts => opts.MapFrom(src => src.Name));

            CreateMap<CommonCommandResponse, CommonModel>();
            CreateMap<ValidatePasswordRecoveryTokenModel, ConfirmRecoverPasswordCommand>();
            CreateMap<LoginModel, SignInQuery>();
        }
    }
}
