using Microsoft.Extensions.Primitives;

namespace Estimatz.UI.Models
{
    public class ValidatePasswordRecoveryTokenModel
    {
        public string UserId { get; set; }
        public string PasswordRecoveryToken { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
