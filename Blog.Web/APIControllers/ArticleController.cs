using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.AppSupport;
using Blog.Web.Models.WebApi;
using Leo.Framework.Models.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.APIControllers
{
    [Produces("application/json")]
    [Route("api/Article")]
    [Authorize]
    public class ArticleController : Controller
    {
        private ILogger _logger;
        private DataContext _dbContext;
        private ISessionManager _sessionManager;
        public ArticleController(DataContext dbContext, ISessionManager sessionManager, ILogger<ArticleController> logger, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _sessionManager = sessionManager;
            _logger = logger;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; set; }

        [HttpPost("Remark")]
        public async Task<JsonResult> Remark(RemarkModel remarkModel)
        {
            var currUser = _sessionManager.CurrUser;
            var logInfo = $"【{currUser.Name}】评论文章";
            try
            {
                //查询当前文章
                Article article = await _dbContext.Article.Where(s => s.Id == remarkModel.ArticleId).Include(s => s.Remarks).FirstOrDefaultAsync();
                if (article == null)
                    return Json(ApiResultAccessor.Unsuccessful);
                if (!article.AllowRemark)
                    return Json(new ApiResult(-1, "文章禁止评论"));
                Remark remark = new Remark();
                remark.Content = remarkModel.EditorContent;
                remark.ArticleId = remarkModel.ArticleId;
                remark.CreateTime = DateTime.Now;
                remark.UserId = currUser.Id;
                remark.Status = 1;
                article.Remarks.Add(remark);
                article.RemarkCount++;
                _dbContext.Article.Update(article);
                logInfo += $"【{article.Title}】";
                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _logger.LogInformation($"{logInfo}-失败");
                    return Json(ApiResultAccessor.Successfully);
                }
                else
                {
                    _logger.LogWarning($"{logInfo}-失败");
                    return Json(ApiResultAccessor.Unsuccessful);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{logInfo}-出错。");
                return Json(ApiResultAccessor.Exception);
            }
        }

        [HttpPost("Reply")]
        public async Task<JsonResult> Reply(ReplyModel replyModel)
        {
            var currUser = _sessionManager.CurrUser;
            try
            {
                //查询目标用户信息
                if (!await _dbContext.User.AnyAsync(s => s.Id == replyModel.TargetUserId))
                    return Json(new ApiResult(-1, "回复的用户不存在"));

                RemarkReply remarkReply = new RemarkReply();
                remarkReply.Content = replyModel.ReplyContent;
                remarkReply.TargetUserId = replyModel.TargetUserId;
                remarkReply.RemarkId = replyModel.RemarkId;
                remarkReply.UserId = currUser.Id;
                remarkReply.CreateTime = DateTime.Now;

                await _dbContext.RemarkReply.AddAsync(remarkReply);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _logger.LogWarning($"用户{currUser.Name}—回复评论—成功");
                    //查询所在文章
                    Article article = await _dbContext.Article.SingleOrDefaultAsync(s => s.Id == replyModel.ArticleId);
                    if (article == null)
                        _logger.LogWarning($"回复评论时—添加用户消息—查询当前文章—NULL。");
                    else
                    {
                        if (!article.AllowReply)
                            return Json(new ApiResult(-1, "文章禁止回复"));
                        //添加用户消息
                        UserMessage userMessage = new UserMessage(replyModel.TargetUserId, Configuration["UserMessageTemp:RemarkReply"].Replace("{UserName}", currUser.Name).Replace("{ArticleId}", replyModel.ArticleId.ToString()).Replace("{ArticleTitle}", article.Title).Replace("{ReplyId}", remarkReply.Id.ToString()), (int)UserMessageType.Remark);
                        await _dbContext.UserMessage.AddAsync(userMessage);
                        if (await _dbContext.SaveChangesAsync() <= 0)
                            _logger.LogWarning($"回复评论—添加用户消息—失败");
                        else
                            _logger.LogInformation($"回复评论—添加用户消息—成功");
                    }
                    return Json(ApiResultAccessor.Successfully);
                }
                else
                {
                    _logger.LogWarning($"用户{currUser.Name}—回复评论—失败");
                    return Json(ApiResultAccessor.Unsuccessful);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"用户{currUser.Name}，回复评论出错。");
                return Json(ApiResultAccessor.Exception);
            }
        }

        [AllowAnonymous]
        [HttpPost("PraiseOrShare")]
        public async Task<JsonResult> PraiseOrShare(int articleId, string type)
        {
            var logInfo = $"文章";
            if (articleId <= 0)
                return Json(ApiResultAccessor.Unsuccessful);
            try
            {
                Article article = await _dbContext.Article.SingleOrDefaultAsync(s => s.Id == articleId && s.Status == (int)CommonStatus.Valid);
                if (article == null)
                    return Json(ApiResultAccessor.Unsuccessful);

                switch (type)
                {
                    case "praise":
                        logInfo += $"【{article.Title}】被点赞";
                        article.PraiseCount++;
                        break;
                    case "share":
                        logInfo += $"【{article.Title}】被分享";
                        article.ShareCount++;
                        break;
                    default:
                        return Json(ApiResultAccessor.Unsuccessful);
                }

                _dbContext.Update(article);
                if (await _dbContext.SaveChangesAsync() <= 0)
                {
                    _logger.LogWarning($"{logInfo}-失败");
                    return Json(ApiResultAccessor.Unsuccessful);
                }
                else
                {
                    _logger.LogInformation($"{logInfo}-成功");
                    return Json(ApiResultAccessor.Successfully);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{logInfo}-出错");
                return Json(ApiResultAccessor.Exception);
            }

        }
    }
}