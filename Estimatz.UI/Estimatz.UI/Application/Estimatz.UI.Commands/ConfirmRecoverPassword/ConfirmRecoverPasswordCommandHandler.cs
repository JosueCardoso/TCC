using AutoMapper;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword;
using MediatR;

namespace Estimatz.UI.Commands.ConfirmRecoverPassword
{
    public class ConfirmRecoverPasswordCommandHandler : IRequestHandler<ConfirmRecoverPasswordCommand, CommonCommandResponse>
    {
        private readonly ILoginApiClient _loginApiClient;
        private readonly IMapper _mapper;

        public ConfirmRecoverPasswordCommandHandler(ILoginApiClient loginApiClient, IMapper mapper)
        {
            _loginApiClient = loginApiClient;
            _mapper = mapper;
        }

        public async Task<CommonCommandResponse> Handle(ConfirmRecoverPasswordCommand command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<ConfirmRecoverPasswordRequest>(command);
            var response = await  _loginApiClient.ConfirmRecoverPassword(request);

            return _mapper.Map<CommonCommandResponse>(response);
        }
    }
}
