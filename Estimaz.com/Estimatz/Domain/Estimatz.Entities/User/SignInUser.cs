namespace Estimatz.Entities.User
{
    public class SignInUser
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserToken UserToken { get; set; }
    }
}
