namespace Estimatz.UI.ExternalServices.EstimatzLoginApi.Register
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }        
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string? ApplicationUrl { get; set; }
    }
}
