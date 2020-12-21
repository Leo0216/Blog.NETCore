using Blog.EntityFramework;
using Blog.Web.AppSupport;
using Blog.Web.SignalR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.IO;

namespace Blog.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // �˷���������ʱ���á�ʹ�ô˷�����������ӵ�������
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //���л�ʱkeyΪ�շ���ʽ
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;    //����ѭ������
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddDbContextPool<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("Default")));

            //ע����֤����
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Domain = ".leo96.com";
                    options.Cookie.Name = "sso";
                    options.Cookie.Path = "/";
                    options.DataProtectionProvider = DataProtectionProvider.Create(new DirectoryInfo(@"c:\blog-shared-auth-ticket-keys\"));
                })
                .AddQQ(qqOptions =>
                {
                    qqOptions.CallbackPath = "/user/callback";
                    qqOptions.ClientId = Configuration["Authentication:QQ:AppId"];
                    qqOptions.ClientSecret = Configuration["Authentication:QQ:AppKey"];
                    //qqOptions.AppId = Configuration["Authentication:QQ:AppId"];
                    //qqOptions.AppKey = Configuration["Authentication:QQ:AppKey"];
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQOpenId, "openid");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQName, "nickname");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQFigure, "figureurl_qq_1");
                    qqOptions.ClaimActions.MapJsonKey(MyClaimTypes.QQGender, "gender");
                });

            //���HttpContext������
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //Ĭ�ϻ���
            services.AddDistributedMemoryCache(); 
            //����Session��30min���ڣ�
            services.AddSession(optain => optain.IdleTimeout = TimeSpan.FromSeconds(1800));
            //ע��Ӧ�ó���Cache����
            services.AddScoped<ICacheManager, CacheManager>();
            //ע��Ӧ�ó���Session����
            services.AddScoped<ISessionManager, SessionManager>();
            //ע��SignalR
            services.AddSignalR();
            //SignalR������
            services.AddSingleton(typeof(ConnectionList));
        }

        // �˷���������ʱ���á�ʹ�ô˷�������http����ܵ���
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // Ĭ��hstsֵΪ30�졣��������ҪΪ�����������Ĵ�����, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapHub<ChatRoom>("/chatroom");
            });
        }
    }
}
