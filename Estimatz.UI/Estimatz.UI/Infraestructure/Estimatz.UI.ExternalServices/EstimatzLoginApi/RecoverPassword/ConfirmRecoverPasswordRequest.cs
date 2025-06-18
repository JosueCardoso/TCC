namespace Estimatz.UI.ExternalServices.EstimatzLoginApi.RecoverPassword
{
    public class ConfirmRecoverPasswordRequest
    {
        public string UserId { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
