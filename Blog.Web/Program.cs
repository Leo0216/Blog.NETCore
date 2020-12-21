using Blog.EntityFramework;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;

namespace Blog.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // NLog: 首先安装记录器以捕获所有错误
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            #region NLog配置
            //设置nlog.config中的变量
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            NLog.LogManager.Configuration.Variables["connectionString"] = configuration.GetConnectionString("Default");
            NLog.LogManager.Configuration.Variables["configDir"] = configuration["AppSettings:LogFilesDir"];
            #endregion

            try
            {
                logger.Debug("init main");

                var host = CreateHostBuilder(args).Build();

                using (var scope = host.Services.CreateScope())
                {
                    var services = scope.ServiceProvider;
                    try
                    {
                        // 数据库初始化
                        //services.GetRequiredService<EntityContext>().Initialize();
                        //services.GetRequiredService<EntityContext>().Database.EnsureDeleted();
                        services.GetRequiredService<DataContext>().Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "An error occurred while seeding the database.");
                        throw ex;
                    }
                }

                logger.Debug("host.Run");
                host.Run();
                //CreateHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                //NLog: 捕获安装错误
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // 确保在应用程序退出前刷新并停止内部计时器/线程（避免Linux上出现分段错误）
                NLog.LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseUrls("http://127.0.0.1:5002")
                    .ConfigureLogging(logging =>
                    {
                        logging.ClearProviders();
                        logging.SetMinimumLevel(LogLevel.Trace);
                    })
                    .UseNLog();
                });
    }
}
