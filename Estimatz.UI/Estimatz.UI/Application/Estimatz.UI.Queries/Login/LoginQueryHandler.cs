using AutoMapper;
using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using Estimatz.UI.ExternalServices.EstimatzLoginApi.Login;
using MediatR;

namespace Estimatz.UI.Queries.Login
{
    public class LoginQueryHandler : IRequestHandler<LoginQuery, LoginQueryResponse>
    {
        private readonly ILoginApiClient _loginApiClient;
        private readonly IMapper _mapper;

        public LoginQueryHandler(ILoginApiClient loginApiClient, IMapper mapper)
        {
            _loginApiClient = loginApiClient;
            _mapper = mapper;
        }

        public async Task<LoginQueryResponse> Handle(LoginQuery command, CancellationToken cancellationToken)
        {
            var request = _mapper.Map<LoginRequest>(command);
            var response = await _loginApiClient.Login(request);

            return _mapper.Map<LoginQueryResponse>(response);
        }
    }
}
