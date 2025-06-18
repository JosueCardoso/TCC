using AutoMapper;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.Commands.ConfirmRecoverPassword;
using Estimatz.UI.Commands.SignIn;
using Estimatz.UI.Models;
using Estimatz.UI.Queries.Login;

namespace Estimatz.UI.Mapping
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<CreateAccountModel, SignInCommand>();
            CreateMap<CommonCommandResponse, CommonModel>();
            CreateMap<ValidatePasswordRecoveryTokenModel, ConfirmRecoverPasswordCommand>();
            CreateMap<LoginModel, LoginQuery>();
        }
    }
}
