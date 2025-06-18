using Estimatz.Entities.Token;

namespace Estimatz.Cache.TokenCache
{
    public interface ITokenMemoryCache
    {
        void Add(string key, SimpleToken value);
        SimpleToken Get(string key);
        void Remove(string key);
        void RemoveExpiredTokens(DateTime dateNow);
    }
}
