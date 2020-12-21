using Blog.Web.AppSupport;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Blog.Web.SignalR
{
    [Authorize]
    public class ChatRoom : Hub
    {
        private ILogger<ChatRoom> Logger => Context.GetHttpContext().RequestServices.GetService(typeof(ILogger<ChatRoom>)) as ILogger<ChatRoom>;

        private ConnectionList Connections => Context.GetHttpContext().RequestServices.GetService(typeof(ConnectionList)) as ConnectionList;

        private ISessionManager SessionManager => Context.GetHttpContext().RequestServices.GetService(typeof(ISessionManager)) as ISessionManager;

        /// <summary>
        /// 客户端连接时触发
        /// </summary>
        /// <returns></returns>
        public override async Task OnConnectedAsync()
        {
            var currUser = SessionManager.CurrUser;
            Connections.Add(currUser);

            Logger.LogInformation($"【{currUser.Name}】加入聊天室");
            //发送在线人数给当前客户端
            await Clients.Caller.SendAsync("OnlineCount", Connections.Count);
            //通知其他客户端有人上线
            await Clients.Others.SendAsync("UserOnline", currUser.Name);
        }
        /// <summary>
        /// 客户端断开连接时触发
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public override async Task OnDisconnectedAsync(Exception ex)
        {
            var currUser = SessionManager.CurrUser;
            Connections.Remove(currUser.Id);

            Logger.LogInformation($"【{currUser.Name}】退出聊天室");
            //通知其他客户端有人下线
            await Clients.Others.SendAsync("UserOffline", currUser.Name);
        }
        /// <summary>
        /// 客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        public void Send(string message)
        {
            if (string.IsNullOrEmpty(message))
                return;
            var currUser = SessionManager.CurrUser;

            Logger.LogInformation($"【{currUser.Name}】在聊天室发送消息：{message}");
            //发送给除自己以外的所有客户端
            Clients.Others.SendAsync("Recive", new { Msg = message, UserAvatar = currUser.Avatar, UserName = currUser.Name, Mine = false });
        }
    }
}
