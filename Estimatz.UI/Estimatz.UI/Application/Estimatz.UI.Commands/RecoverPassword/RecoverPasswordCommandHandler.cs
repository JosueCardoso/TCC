using AutoMapper;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword;
using MediatR;

namespace Estimatz.UI.Commands.RecoverPassword
{
    public class RecoverPasswordCommandHandler : IRequestHandler<RecoverPasswordCommand, CommonCommandResponse>
    {
        private readonly ILoginApiClient _loginApiClient;
        private readonly IMapper _mapper;

        public RecoverPasswordCommandHandler(ILoginApiClient loginApiClient, IMapper mapper)
        {
            _loginApiClient = loginApiClient;
            _mapper = mapper;
        }

        public async Task<CommonCommandResponse> Handle(RecoverPasswordCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<RecoverPasswordRequest>(command);
            request.ApplicationURL = "https://localhost:7098/Account/ValidatePasswordRecoveryToken";
            var response = await _loginApiClient.RecoverPassword(request);

            return _mapper.Map<CommonCommandResponse>(response);
        }
    }
}
