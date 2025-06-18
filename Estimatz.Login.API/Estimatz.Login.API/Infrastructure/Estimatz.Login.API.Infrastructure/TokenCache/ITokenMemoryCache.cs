using Estimatz.Login.API.Entities.Token;

namespace Estimatz.Login.API.Data.TokenCache
{
    public interface ITokenMemoryCache
    {
        void Add(string key, SimpleToken value);
        SimpleToken Get(string key);
        void Remove(string key);
        void RemoveExpiredTokens(DateTime dateNow);
    }
}
