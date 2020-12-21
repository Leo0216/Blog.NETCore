using Microsoft.Extensions.Caching.Distributed;
using System;

namespace Blog.Web.AppSupport
{
    public class CacheManager : ICacheManager
    {
        private IDistributedCache _cache;
        public CacheManager(IDistributedCache cache)
        {
            _cache = cache;
        }
        public void Remove(CacheKey cacheKey)
        {
            throw new NotImplementedException();
        }
    }
}
