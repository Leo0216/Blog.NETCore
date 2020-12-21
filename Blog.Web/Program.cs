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
            // NLog: ���Ȱ�װ��¼���Բ������д���
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            #region NLog����
            //����nlog.config�еı���
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
                        // ���ݿ��ʼ��
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
                //NLog: ����װ����
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                // ȷ����Ӧ�ó����˳�ǰˢ�²�ֹͣ�ڲ���ʱ��/�̣߳�����Linux�ϳ��ֶַδ���
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
