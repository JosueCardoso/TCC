using Estimatz.Login.API.Entities.Token;
using System.Collections.Concurrent;

namespace Estimatz.Login.API.Data.TokenCache
{
    public class TokenMemoryCache : ITokenMemoryCache
    {
        private ConcurrentDictionary<string, SimpleToken> _usersTokens;
        public TokenMemoryCache()
        {
            _usersTokens = new ConcurrentDictionary<string, SimpleToken>();
        }

        public void Add(string key, SimpleToken value)
        {
            _usersTokens.TryAdd(key, value);
        }

        public SimpleToken Get(string key)
        {
            _usersTokens.TryGetValue(key, out SimpleToken value);
            return value;
        }

        public void Remove(string key)
        {
            _usersTokens.TryRemove(key, out _);
        }

        public List<SimpleToken> GetAllTokens() => _usersTokens.Values.ToList();

        public void RemoveExpiredTokens(DateTime dateNow)
        {
            var expiredTokens = _usersTokens.Where(x => x.Value.ExpireAt < dateNow).ToList();
            expiredTokens.ForEach(x => Remove(x.Value.TokenString));
        }
    }
}
