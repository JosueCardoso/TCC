namespace Estimatz.Login.API.Entities.Token
{
    public class SimpleToken
    {
        public string UserId { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpireAt { get; set; }
    }
}
