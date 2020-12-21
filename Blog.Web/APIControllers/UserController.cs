using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.AppSupport;
using Leo.Framework.Models.WebAPI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Web.APIControllers
{
    [Produces("application/json")]
    [Route("api/User")]
    [Authorize]
    public class UserController : Controller
    {
        private ILogger _logger;
        private DataContext _dbContext;
        private ISessionManager _sessionManager;
        public UserController(DataContext dbContext, ISessionManager sessionManager, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _sessionManager = sessionManager;
            _logger = logger;
        }

        /// <summary>
        /// 获取当前用户未读消息
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUnreadMsg")]
        public async Task<JsonResult> GetUnreadMsg()
        {
            var currUser = _sessionManager.CurrUser;
            try
            {
                var userMessageList = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status == (int)UserMessageStatus.Unread).ToListAsync();

                return Json(new ApiResultWithData<List<UserMessage>>(userMessageList, 1, "成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"【{currUser.Name}】—获取未读消息—异常");
                return Json(ApiResultAccessor.Unsuccessful);
            }
        }

        /// <summary>
        /// 获取当前用户全部消息（调用此接口将使对应类型消息全部标记为已读）
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllMsg")]
        public async Task<JsonResult> GetAllMsg(string type)
        {
            var currUser = _sessionManager.CurrUser;
            try
            {
                List<UserMessage> userMessageList = null;
                switch (type)
                {
                    case "pl":
                        userMessageList = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status != (int)UserMessageStatus.Invalid && s.MessageType == (int)UserMessageType.Remark).OrderByDescending(s => s.CreateTime).Take(20).ToListAsync();
                        break;
                    case "ly":
                        userMessageList = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status != (int)UserMessageStatus.Invalid && s.MessageType == (int)UserMessageType.Comment).OrderByDescending(s => s.CreateTime).Take(20).ToListAsync();
                        break;
                    case "xt":
                        userMessageList = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status != (int)UserMessageStatus.Invalid && s.MessageType == (int)UserMessageType.System).OrderByDescending(s => s.CreateTime).Take(20).ToListAsync();
                        break;
                    default:
                        userMessageList = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status != (int)UserMessageStatus.Invalid).OrderByDescending(s => s.CreateTime).Take(40).ToListAsync();
                        break;
                }
                //筛选未读消息
                var unreadMessage = userMessageList.Where(s => s.Status == (int)UserMessageStatus.Unread).ToList();
                foreach (var item in unreadMessage)
                {
                    item.Status = (int)UserMessageStatus.Read;
                }
                //更新为已读
                _dbContext.UpdateRange(unreadMessage);
                //提交更改
                if (await _dbContext.SaveChangesAsync() < unreadMessage.Count)
                    _logger.LogWarning($"用户[{currUser.Id}]【{currUser.Name}】—标记消息为已读—受影响行数小于未读消息条数");

                return Json(new ApiResultWithData<List<UserMessage>>(userMessageList, 1, "成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"用户[{currUser.Id}]【{currUser.Name}】—获取全部消息—异常");
                return Json(ApiResultAccessor.Unsuccessful);
            }
        }

        /// <summary>
        /// 获取当前用户未读消息数量
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetUnreadMsgCnt")]
        public async Task<JsonResult> GetUnreadMsgCnt()
        {
            var currUser = _sessionManager.CurrUser;
            try
            {
                var count = await _dbContext.UserMessage.Where(s => s.TargetUserId == currUser.Id && s.Status == (int)UserMessageStatus.Unread).CountAsync();

                return Json(new ApiResultWithData<int>(count, 1, "成功"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"用户[{currUser.Id}]【{currUser.Name}】—获取未读消息数量—异常");
                return Json(ApiResultAccessor.Unsuccessful);
            }
        }
    }
}