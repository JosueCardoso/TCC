using AutoMapper;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Register;
using MediatR;

namespace Estimatz.UI.Commands.SignIn
{
    public class SignInCommandHandler : IRequestHandler<SignInCommand, CommonCommandResponse>
    {
        private readonly ILoginApiClient _loginApiClient;
        private readonly IMapper _mapper;

        public SignInCommandHandler(ILoginApiClient loginApiClient, IMapper mapper)
        {
            _loginApiClient = loginApiClient;
            _mapper = mapper;
        }

        public async Task<CommonCommandResponse> Handle(SignInCommand command, CancellationToken cancellationToken)
        {
            var requestApi = _mapper.Map<RegisterRequest>(command);
            requestApi.ApplicationUrl = "https://localhost:7098/Account/ConfirmCreateAccount";
            var responseApi = await _loginApiClient.Register(requestApi);

            return _mapper.Map<CommonCommandResponse>(responseApi);
        }
    }
}
