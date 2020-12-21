using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.AppSupport;
using Blog.Web.Models;
using Leo.Framework.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Blog.Web.Controllers
{
    public class UserController : Controller
    {
        private DataContext _dbContext;
        private ISessionManager _sessionManager;
        private ILogger _logger;
        public UserController(DataContext dbContext, IConfiguration configuration, ISessionManager sessionManager, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            Configuration = configuration;
            _sessionManager = sessionManager;
            _logger = logger;
        }

        public IConfiguration Configuration { get; set; }

        /// <summary>
        /// 第三方登录
        /// </summary>
        /// <param name="provider">第三方类型</param>
        /// <param name="returnUrl">登录成功后重定向地址</param>
        /// <returns></returns>
        public IActionResult Login(string provider = "QQ", string returnUrl = null)
        {
            //第三方登录成功后跳转的地址
            var redirectUrl = Url.Action("ExternalLoginCallback", new { returnUrl });
            var properties = new AuthenticationProperties()
            {
                RedirectUri = redirectUrl
            };
            return Challenge(properties, provider);
        }

        /// <summary>
        /// 第三方登录回调
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="remoteError"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> ExternalLoginCallbackAsync(string returnUrl = null)
        {
            //先注销当前用第三方登录的身份
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            string openId = "", name = "", figure = "", gender = "";

            foreach (var item in HttpContext.User.Claims)
            {
                switch (item.Type)
                {
                    case ClaimTypes.NameIdentifier: //新版QQ登录
                        openId = item.Value;
                        break;
                    case MyClaimTypes.QQOpenId:
                        openId = item.Value;
                        break;
                    case MyClaimTypes.QQName:
                        name = item.Value;
                        break;
                    case MyClaimTypes.QQFigure:
                        figure = item.Value;
                        break;
                    case MyClaimTypes.QQGender:
                        gender = item.Value;
                        break;
                    default:
                        break;
                }
            }

            if (!openId.IsNullOrEmpty())
            {
                //去数据库查询该QQ是否绑定用户
                User user = await _dbContext.User.Where(s => s.QQOpenId == openId).FirstOrDefaultAsync();
                if (user != null)
                {
                    #region 登陆
                    var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                    identity.AddClaim(new Claim(ClaimTypes.Sid, user.Id.ToString()));
                    identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                    identity.AddClaim(new Claim(MyClaimTypes.Avator, user.Avatar));
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(int.Parse(Configuration["AppSettings:LoginExpires"]))) // 有效时间
                    });
                    user.LastLoginIP = HttpContext.GetUserIP();
                    user.LastLoginTime = DateTime.Now;
                    //更新登录信息
                    _dbContext.User.Update(user);
                    await _dbContext.SaveChangesAsync();
                    #endregion

                    //激活当前用户Session
                    //var currUser = _sessionManager.CurrUser;
                    _logger.LogInformation($"【{user.Name}】登录网站-成功");
                    if (returnUrl != null)
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("index", "home");
                    }
                }
                else
                {
                    User userModel = new User();
                    userModel.QQOpenId = openId;
                    userModel.Name = name;
                    userModel.Avatar = figure.Replace("http://","https://");
                    userModel.Gender = gender;
                    userModel.CreateTime = DateTime.Now;
                    userModel.Remark = "";
                    userModel.Status = (int)CommonStatus.Valid;
                    //注册
                    await _dbContext.User.AddAsync(userModel);
                    if (await _dbContext.SaveChangesAsync() > 0)
                    {

                        #region 登陆
                        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                        identity.AddClaim(new Claim(ClaimTypes.Sid, userModel.Id.ToString()));
                        identity.AddClaim(new Claim(ClaimTypes.Name, userModel.Name));
                        identity.AddClaim(new Claim(MyClaimTypes.Avator, userModel.Avatar));
                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.Now.Add(TimeSpan.FromDays(int.Parse(Configuration["AppSettings:LoginExpires"]))) // 有效时间
                        });
                        userModel.LastLoginIP = HttpContext.GetUserIP();
                        userModel.LastLoginTime = DateTime.Now;
                        //更新登录信息
                        _dbContext.User.Update(userModel);
                        await _dbContext.SaveChangesAsync();
                        #endregion

                        //激活当前用户Session
                        //var currUser = _sessionManager.CurrUser;
                        _logger.LogInformation($"【{userModel.Name}】登录网站-成功");

                        if (returnUrl != null)
                        {
                            return Redirect(returnUrl);
                        }
                        else
                        {
                            return RedirectToAction("index", "home");
                        }
                    }
                    else
                    {
                        throw new Exception("Add User failed");
                    }
                }

            }
            else
            {
                throw new Exception("OpenId is null");
            }
        }

        /// <summary>
        /// 注销登录
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Logout(string returnUrl)
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _sessionManager.Remove(SessionKey.CurrUser);
            if (!returnUrl.IsNullOrEmpty())
                return Redirect(returnUrl);
            else
                return RedirectToAction("index", "home");
        }

        [Authorize]
        public async Task<IActionResult> Zone()
        {
            var currUser = _sessionManager.CurrUser;
            try
            {
                var commonQuery = _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status == (int)UserMessageStatus.Unread);

                ZoneModel zoneModel = new ZoneModel();

                zoneModel.RemarkCount = await commonQuery.Where(s => s.MessageType == (int)UserMessageType.Remark).CountAsync(); //评论消息未读数量
                zoneModel.CommentCount = await commonQuery.Where(s => s.MessageType == (int)UserMessageType.Comment).CountAsync(); //留言消息未读数量
                zoneModel.SystemCount = await commonQuery.Where(s => s.MessageType == (int)UserMessageType.System).CountAsync(); //系统消息未读数量

                return View(zoneModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}