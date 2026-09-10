using Domain.Autentication.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Threading.Tasks;

namespace Infrastructure.CrossCutting.Autentication.Secrets.CacheSecrect
{
    public class CachedSecretProvider : ISecretProvider
    {
        private readonly ISecretProvider _innerSecretProvider;
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration;

        public CachedSecretProvider(ISecretProvider innerSecretProvider, IMemoryCache cache, TimeSpan cacheDuration)
        {
            _innerSecretProvider = innerSecretProvider;
            _cache = cache;
            _cacheDuration = cacheDuration;
        }

        public async Task<string> GetSecretAsync(string secretName)
        {
            return await _cache.GetOrCreateAsync(secretName, async entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = _cacheDuration;
                return await _innerSecretProvider.GetSecretAsync(secretName).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}
