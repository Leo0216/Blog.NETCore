using Blog.EntityFramework;
using Blog.EntityFramework.Enum;
using Blog.Web.AppSupport;
using Leo.Framework.Models.WebAPI;
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
    [Route("api/All")]
    public class AllController : Controller
    {
        private ILogger _logger;
        private DataContext _dbContext;
        private ISessionManager _sessionManager;
        public AllController(DataContext dbContext, ILogger<AllController> logger, ISessionManager sessionManager, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _logger = logger;
            _sessionManager = sessionManager;
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; set; }

        [HttpGet("GetNotes")]
        public async Task<JsonResult> GetNotes(int pageSize = 8, int pageIndex = 1)
        {
            try
            {
                var count = await _dbContext.Note.Where(s => s.Status == (int)CommonStatus.Valid).CountAsync();

                var data = await _dbContext.Note.Where(s => s.Status == (int)CommonStatus.Valid).Skip(pageSize * (pageIndex - 1)).Take(pageSize).OrderByDescending(s => s.CreateTime).ToListAsync();

                return Json(new { Data = data, Code = 1, Msg = "OK", Count = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "获取笔记-出错");
                return Json(ApiResultAccessor.Exception);
            }
        }
    }
}