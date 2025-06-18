using AutoMapper;
using Estimatz.UI.Commands.Common;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using MediatR;

namespace Estimatz.UI.Commands.ConfirmEmailSignIn
{
    public class ConfirmEmailSignInCommandHandler : IRequestHandler<ConfirmEmailSignInCommand, CommonCommandResponse>
    {
        private readonly ILoginApiClient _loginApiClient;
        private readonly IMapper _mapper;
        public ConfirmEmailSignInCommandHandler(ILoginApiClient loginApiClient, IMapper mapper)
        {
            _loginApiClient = loginApiClient;
            _mapper = mapper;
        }

        public async Task<CommonCommandResponse> Handle(ConfirmEmailSignInCommand command, CancellationToken cancellationToken)
        {
            var responseApi = await _loginApiClient.ConfirmEmail(command.UserId, command.Token);
            return _mapper.Map<CommonCommandResponse>(responseApi); 
        }
    }
}
