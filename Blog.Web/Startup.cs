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

        // 此方法由运行时调用。使用此方法将服务添加到容器。
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver(); //序列化时key为驼峰样式
                    options.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                    options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    options.SerializerSettings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;    //忽略循环引用
                    options.SerializerSettings.Formatting = Formatting.Indented;
                });

            services.AddDbContextPool<DataContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("Default")));

            //注册认证服务
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

            //添加HttpContext访问器
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //默认缓存
            services.AddDistributedMemoryCache(); 
            //启用Session（30min过期）
            services.AddSession(optain => optain.IdleTimeout = TimeSpan.FromSeconds(1800));
            //注册应用程序Cache管理
            services.AddScoped<ICacheManager, CacheManager>();
            //注册应用程序Session管理
            services.AddScoped<ISessionManager, SessionManager>();
            //注册SignalR
            services.AddSignalR();
            //SignalR连接类
            services.AddSingleton(typeof(ConnectionList));
        }

        // 此方法由运行时调用。使用此方法配置http请求管道。
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // 默认hsts值为30天。您可能需要为生产场景更改此设置, see https://aka.ms/aspnetcore-hsts.
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
