using Blog.EntityFramework;

namespace Blog.Web.AppSupport
{
    public interface ISessionManager
    {
        /// <summary>
        /// 当前用户
        /// </summary>
        User CurrUser { get; }
        /// <summary>
        /// 移除Session
        /// </summary>
        void Remove(SessionKey sessionKey);
    }
}
