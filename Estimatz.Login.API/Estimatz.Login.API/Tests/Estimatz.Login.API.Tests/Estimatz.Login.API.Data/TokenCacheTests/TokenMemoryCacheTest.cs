using Estimatz.Login.API.Data.TokenCache;
using Estimatz.Login.API.Entities.Token;
using FluentAssertions;

namespace Estimatz.Login.API.Tests.Estimatz.Login.API.Data.TokenCacheTests
{
    public class TokenMemoryCacheTest
    {
        [Fact]
        public void DeveAdicionarTokenNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleTokken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1",simpleTokken);
            tokenCache.Add("token2", simpleToken2);

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(2);
        }

        [Fact]
        public void DeveObterTokenNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleToken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1", simpleToken);
            tokenCache.Add("token2", simpleToken2);
            var result = tokenCache.Get("token1");

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(2);
            result.Should().BeEquivalentTo(simpleToken); 
        }

        [Fact]
        public void NaoDeveObterTokenNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleToken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1", simpleToken);
            tokenCache.Add("token2", simpleToken2);
            var result = tokenCache.Get("token3");

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(2);
            result.Should().BeNull();
        }

        [Fact]
        public void DeveRemoverTokenNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleToken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1", simpleToken);
            tokenCache.Add("token2", simpleToken2);
            tokenCache.Remove("token2");
            var result = tokenCache.Get("token1");
            var result2 = tokenCache.Get("token2");

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(1);
            result.Should().BeEquivalentTo(simpleToken);
            result2.Should().BeNull();
        }

        [Fact]
        public void NaoDeveRemoverTokenNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleToken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow,
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1", simpleToken);
            tokenCache.Add("token2", simpleToken2);
            tokenCache.Remove("token3");
            var result = tokenCache.Get("token1");
            var result2 = tokenCache.Get("token2");

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(2);
            result.Should().BeEquivalentTo(simpleToken);
            result2.Should().BeEquivalentTo(simpleToken2);
        }

        [Fact]
        public void DeveFazerALimpezaNoCache()
        {
            //arrange
            var tokenCache = new TokenMemoryCache();
            var simpleToken = new SimpleToken
            {
                UserId = "userId1",
                ExpireAt = DateTime.UtcNow.AddMinutes(5),
                TokenString = "token1"
            };

            var simpleToken2 = new SimpleToken
            {
                UserId = "userId2",
                ExpireAt = DateTime.UtcNow.AddMinutes(-1),
                TokenString = "token2"
            };

            //act
            tokenCache.Add("token1", simpleToken);
            tokenCache.Add("token2", simpleToken2);
            tokenCache.RemoveExpiredTokens(DateTime.UtcNow);
            var result = tokenCache.Get("token1");
            var result2 = tokenCache.Get("token2");

            //assert
            tokenCache.GetAllTokens().Count.Should().Be(1);
            result.Should().BeEquivalentTo(simpleToken);
            result2.Should().BeNull();
        }
    }
}
