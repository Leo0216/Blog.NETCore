namespace Blog.Web.AppSupport
{
    public interface ICacheManager
    {
        void Remove(CacheKey cacheKey);
    }
}
