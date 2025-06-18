using Microsoft.Extensions.Hosting;

namespace Estimatz.Cache.TokenCache
{
    public class TokenCacheCleaner : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly ITokenMemoryCache _tokenCache;

        public TokenCacheCleaner(ITokenMemoryCache tokenCache)
        {
            _tokenCache = tokenCache;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            // remove expired refresh tokens from cache every minute
            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            _tokenCache.RemoveExpiredTokens(DateTime.UtcNow);
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
