using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.AppSupport;
using Blog.Web.Models.WebApi;
using Leo.Framework.Extensions;
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
    [Route("api/Comment")]
    [Authorize]
    public class CommentController : Controller
    {

        private ILogger _logger;
        private DataContext _dbContext;
        private ISessionManager _sessionManager;
        public CommentController(DataContext dbContext, ILogger<ArticleController> logger, ISessionManager sessionManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _sessionManager = sessionManager;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        [HttpGet("GetCommentsByPage")]
        [AllowAnonymous]
        public async Task<JsonResult> GetCommentsByPage(int pageIndex, int pageSize)
        {
            try
            {
                var count = await _dbContext.Comment.Where(s => s.Status == (int)CommonStatus.Valid).CountAsync();

                var data = await _dbContext.Comment
                    .Include(s => s.User)
                    .Include(s => s.CommentReplys).ThenInclude(s => s.TargetUser)
                    .Include(s => s.CommentReplys).ThenInclude(s => s.User)
                    .Where(s => s.Status == (int)CommonStatus.Valid)
                    .OrderByDescending(s => s.CreateTime)
                    .Skip(pageSize * (pageIndex - 1))
                    .Take(pageSize)
                    .ToListAsync();

                return Json(new { Code = 1, Msg = "成功", Count = count, Data = data });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "分页获取留言 - 出错");
                return Json(ApiResultAccessor.Exception);
            }
        }

        [HttpPost("Add")]
        public async Task<JsonResult> Add(string content)
        {
            var logInfo = $"【{_sessionManager.CurrUser.Name}】留言";
            if (content.IsNullOrWhiteSpace())
                return Json(new ApiResult(-1, "留言内容不能为空"));
            if (content.Length > 500)
                return Json(new ApiResult(-1, "内容必须少于500个字符"));
            try
            {
                var currUser = _sessionManager.CurrUser;
                if (currUser == null)
                    return Json(ApiResultAccessor.Unauthorized);
                var comment = new Comment()
                {
                    Content = content,
                    CreateTime = DateTime.Now,
                    Status = (int)CommonStatus.Valid,
                    UserId = currUser.Id
                };
                await _dbContext.Comment.AddAsync(comment);
                if (await _dbContext.SaveChangesAsync() <= 0)
                {
                    _logger.LogWarning($"{logInfo}-受影响行数小于等于0");
                    return Json(ApiResultAccessor.Unsuccessful);
                }
                else
                    _logger.LogInformation($"{logInfo}-成功");
                //查询当前留言信息，返回给客户端
                var data = await _dbContext.Comment.Where(s => s.Id == comment.Id).Include(s => s.User).SingleOrDefaultAsync();
                return Json(new ApiResultWithData<Comment>(data, 1, "操作成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{logInfo}-出错");
                return Json(ApiResultAccessor.Exception);
            }
        }

        [HttpPost("Reply")]
        public async Task<JsonResult> Reply(ReplyModel replyModel)
        {
            var currUser = _sessionManager.CurrUser;
            var logInfo = $"【{currUser.Name}】回复留言";
            try
            {
                //信息验证
                if (replyModel.ReplyContent.IsNullOrWhiteSpace())
                    return Json(new ApiResult(-1, "回复内容不能为空"));
                if (replyModel.ReplyContent.Length > 500)
                    return Json(new ApiResult(-1, "内容必须少于500个字符"));
                if (replyModel.RemarkId == 0)
                    return Json(new ApiResult(-1, "回复失败"));
                //查询目标用户信息
                if (!await _dbContext.User.AnyAsync(s => s.Id == replyModel.TargetUserId))
                    return Json(new ApiResult(-1, "回复的用户不存在"));
                CommentReply commnetReply = new CommentReply();
                commnetReply.Content = replyModel.ReplyContent;
                commnetReply.TargetUserId = replyModel.TargetUserId;
                commnetReply.CommentId = replyModel.RemarkId;
                commnetReply.UserId = currUser.Id;
                commnetReply.CreateTime = DateTime.Now;

                await _dbContext.CommentReply.AddAsync(commnetReply);

                if (await _dbContext.SaveChangesAsync() > 0)
                {
                    _logger.LogInformation($"{logInfo}-成功");
                    //添加用户消息
                    UserMessage userMessage = new UserMessage(replyModel.TargetUserId, Configuration["UserMessageTemp:CommentReply"].Replace("{UserName}", currUser.Name).Replace("{Content}", replyModel.ReplyContent), (int)UserMessageType.Comment);
                    await _dbContext.UserMessage.AddAsync(userMessage);
                    if (await _dbContext.SaveChangesAsync() <= 0)
                        _logger.LogWarning($"回复留言—添加用户消息—失败");
                    //查询刚才添加的回复信息返回给客户端
                    var reply = await _dbContext.CommentReply.Where(s => s.Id == commnetReply.Id).Include(s => s.TargetUser).Include(s => s.User).SingleOrDefaultAsync();
                    return Json(new ApiResultWithData<CommentReply>(reply, 1, "操作成功"));
                }
                else
                {
                    _logger.LogWarning($"{logInfo}-失败");
                    return Json(ApiResultAccessor.Unsuccessful);
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