using Estimatz.UI.ExternalServices.EstimatzLoginApi;
using MediatR;

namespace Estimatz.UI.Queries.ValidateUser
{
    public class ValidateUserQueryHandler : IRequestHandler<ValidateUserQuery, bool>
    {
        private readonly ILoginApiClient _loginApiClient;

        public ValidateUserQueryHandler(ILoginApiClient loginApiClient)
        {
            _loginApiClient = loginApiClient;
        }

        public async Task<bool> Handle(ValidateUserQuery request, CancellationToken cancellationToken)
        {
            var response = await _loginApiClient.ValidateUser(request.UserId, request.Token);
            return response.Success;
        }
    }
}
