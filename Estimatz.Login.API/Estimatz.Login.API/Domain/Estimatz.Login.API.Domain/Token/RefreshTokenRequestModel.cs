namespace Estimatz.Login.API.Entities.Token
{
    public class RefreshTokenRequestModel
    {
        public string Email { get; set; }
        public string TokenString { get; set; }
    }
}
