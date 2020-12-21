using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System.Security.Claims;
using Leo.Framework.Extensions;

namespace Blog.Web.AppSupport
{
    public class SessionManager : ISessionManager
    {
        const string CURRUSER = "CURRUSER";
        const string CURRUSERID = "CURRUSERID";

        private IHttpContextAccessor httpContextAccessor;
        private DataContext _dbContext;
        public SessionManager(IHttpContextAccessor httpContextAccessor, DataContext dbContext)
        {
            this.httpContextAccessor = httpContextAccessor;
            _dbContext = dbContext;
        }

        public User CurrUser
        {
            get
            {
                var user = httpContextAccessor.HttpContext.Session.GetObject<User>(CURRUSER);

                if (user == null && httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    Claim claim = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Sid);
                    if (claim != null)
                    {
                        user = _dbContext.User.Where(s => s.Id == int.Parse(claim.Value) && s.Status == (int)CommonStatus.Valid).FirstOrDefault();
                        httpContextAccessor.HttpContext.Session.SetObject(CURRUSER, user);
                        httpContextAccessor.HttpContext.Session.SetString(CURRUSERID, user.Id.ToString());
                    }
                }
                return user;
            }
        }

        public void Remove(SessionKey sessionKey)
        {
            switch (sessionKey)
            {
                case SessionKey.CurrUser:
                    httpContextAccessor.HttpContext.Session.Remove(CURRUSER);
                    httpContextAccessor.HttpContext.Session.Remove(CURRUSERID);
                    break;
                default:
                    break;
            }
        }
    }
}
