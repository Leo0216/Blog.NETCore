/*---------------------------------------------------------------------------------- 
 * 类 名 称：DataContext
 * 类 描 述：数据上下文类
 * 作    者：Leo
 * 创建时间：2019/9/25 16:47:01
 * 更新时间：2019/9/25 16:47:01
 * 版 本 号：v1.0.0
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 * Copyright @ Leo 2019. All rights reserved.
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 ----------------------------------------------------------------------------------*/
using Blog.EntityFramework.Admin;
using Microsoft.EntityFrameworkCore;
using System;

namespace Blog.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SystemLog> SystemLog { get; set; }
        public DbSet<Article> Article { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<ArticleCategory> ArticleCategory { get; set; }
        public DbSet<Music> Music { get; set; }
        public DbSet<Remark> Remark { get; set; }
        public DbSet<RemarkReply> RemarkReply { get; set; }
        public DbSet<UserMessage> UserMessage { get; set; }
        public DbSet<Announcement> Announcement { get; set; }
        public DbSet<Carousel> Carousel { get; set; }
        public DbSet<Attachment> Attachment { get; set; }
        public DbSet<Timeline> Timeline { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Production> Production { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<CommentReply> CommentReply { get; set; }
        public DbSet<FriendLink> FriendLink { get; set; }

        #region Admin
        public DbSet<Administrator> Administrator { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<LoginLog> LoginLog { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<RoleMenu> RoleMenu { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 种子数据
            var articleCategorys = new ArticleCategory[] {
                new ArticleCategory{ Id=1,Name =".NET Core",CreateTime=DateTime.Now,Order=1,Status=1 },
                new ArticleCategory{ Id=2,Name ="Web前端",CreateTime=DateTime.Now,Order=2,Status=1 }
            };

            var articles = new Article[] {
                new Article{ Id=1,Abstract="今天凌晨微软在.NET Conf上正式发布了.NET Core 3.0，2018年12月4日，微软推出首个预览版.NET Core 3.0 Preview1，时至今日，.NET Core 3.0正式版终于发布！",CategoryId=1,Content="<p>同时发布的还有C# 8.0、ASP.NET Core 3.0、EF Core 3.0、Visual Studio 2019 16.3、Visual Studio 2019 for mac 8.3。</p><p>.NET Core 3.0是Visual Studio 2019 16.3的一部分，只需升级到Visual Studio 2019 16.3就可以获取.NET Core 3.0。</p><p><span style=\"color: rgb(0, 0, 0); font-weight: bold;\">.Net Core 3.0下载</span></p><ul><li><p>●&nbsp;<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://dotnet.microsoft.com/download/dotnet-core/3.0\" target=\"_blank\">.NET Core 3.0 SDK and Runtime</a></p></li><li><p>●&nbsp;<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://snapcraft.io/dotnet-sdk\" target=\"_blank\">Snap installer</a></p></li><li><p>●&nbsp;<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://hub.docker.com/_/microsoft-dotnet-core\" target=\"_blank\">Docker images</a></p></li></ul><blockquote><span style=\"font-size: large;\">关于.NET Core 3.0</span></blockquote><p>以下几点是.NET Core 3.0关键的改进，需要引起注意：</p><p>&nbsp;&nbsp;&nbsp;&nbsp;<span style=\"color: rgb(0, 0, 0); font-weight: bold;\">● .NET Core 3.0在dot.net和Bing.com上测验</span>了几个月，已经通过了测试。许多其他Microsoft团队很快将在生产中的.NET Core 3.0上部署大型工作负载。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">● 许多组件的性能都得到了极大的提高</span>，并在<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://devblogs.microsoft.com/dotnet/performance-improvements-in-net-core-3-0/\" target=\"_blank\">Performance Improvements in .NET Core 3.0</a>进行了详细描述。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">● C# 8.0</span>增加了异步流（async streams）、范围（Range）和索引（Index）、更多的模式以及可空引用类型（Nullable）。可空引用类型主要针对导致代码缺陷的空指针异常（NullReferenceException）</p><p>&nbsp; &nbsp; <span style=\"color: rgb(0, 0, 0); font-weight: bold;\">● F# 4.7</span>发布，详情请看：<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://devblogs.microsoft.com/dotnet/announcing-f-4-7/\" target=\"_blank\">Announcing F# 4.7</a></p><p>&nbsp; &nbsp; <span style=\"color: rgb(0, 0, 0); font-weight: bold;\">● .NET Standard 2.1</span>增加了可以在.NET Core和Xamarin代码中都可以使用的类型集，.NET Standard 2.1包含自.NET Core 2.1以来的类型。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">● .NET Core已添加对Window桌面应用的支持</span>，包括Windows Form和WPF（已开源）。WPF设计器已经是Visual Studio 2019 16.3的一部分，Windows Form设计器仍处于预览版，可从<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://aka.ms/winforms-designer\" target=\"_blank\">VSIX进行下载</a>。</p><p>&nbsp; &nbsp; <span style=\"color: rgb(0, 0, 0); font-weight: bold;\">●&nbsp;.NET Core 应用现在默认情况下就具有可执行文件</span>。以前的应用需要使用dotnet命令来启动（如dotnet myapp.dll），现在可以使用特定于应用程序的可执行文件来启动应用程序（如myapp或./myapp），具体取决于操作系统。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">●&nbsp;新增高性能的JSON API</span>，用于读取器/写入器，对象模型和序列化方案。这些 API 基于Span&lt;T&gt;从头开始构建，使用 UTF8 来替代 UTF16（如 string），由于这些 API 使用了最小化的内存分配，因此带来了更好的性能，减少了垃圾回收器的工作。详情请看<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://github.com/dotnet/corefx/issues/33115\" target=\"_blank\">The future of JSON in .NET Core 3.0</a>。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">●&nbsp;默认情况下，垃圾回收使用更少的内存</span>。这种改进对于许多应用程序托管在同一服务器上的场景非常有益。垃圾收集器（GC）也进行了更新，以更好地利用大于64核的机器上的大量核心。</p><p>&nbsp; &nbsp;<span style=\"font-weight: bold; color: rgb(0, 0, 0);\"> ●&nbsp;.NET Core已针对Docker进行了强化</span>，以使.NET应用程序在容器中可预测且有效地工作。已将容器配置为有限的内存或CPU时，垃圾收集器和线程池已更新为更好地工作。.NET Core的Docker镜像体积较小，尤其是SDK镜像。</p><p>&nbsp; &nbsp; <span style=\"font-weight: bold; color: rgb(0, 0, 0);\">●&nbsp;现在支持Raspberry Pi和ARM芯片</span>以支持IoT开发，包括使用远程Visual Studio调试器。可以使用新的GPIO API部署可监听传感器的应用程序，并在显示器上打印消息或图像。ASP.NET可用于将数据公开为API或允许配置IoT设备的站点。</p><p>&nbsp; &nbsp; ●&nbsp;.NET Core 3.0 会被 11 月发布的 .NET Core 3.1 取代，因为后者才是 LTS 版本（至少会获得三年的技术支持），不过官方还是建议大家先升级到 .NET Core 3.0，然后再升级到 3.1，这样过渡起来更方便。</p><p>&nbsp; &nbsp; ●&nbsp;.NET Core 2.2将于12/23停止服务。请参阅<a style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\" href=\"https://dotnet.microsoft.com/platform/support/policy/dotnet-core\" target=\"_blank\">.NET Core支持策略</a>。</p><p>&nbsp; &nbsp; ●&nbsp;对于要使用.NET Core 3.0的Windows上的Visual Studio用户，Visual Studio 2019 16.3是必需的更新。</p><p>&nbsp; &nbsp; ●&nbsp;对于要使用.NET Core 3.0的Mac用户，Visual Studio for Mac 8.3是必需的更新。</p><blockquote><span style=\"font-size: large;\">平台支持</span></blockquote><p>以下操作系统支持.NET Core 3.0：&nbsp;</p><p>&nbsp;Alpine: 3.9+&nbsp;</p><p>&nbsp;Debian: 9+&nbsp;</p><p>&nbsp;openSUSE: 42.3+&nbsp;</p><p>&nbsp;Fedora: 26+&nbsp;</p><p>&nbsp;Ubuntu: 16.04+&nbsp;</p><p>&nbsp;RHEL: 6+&nbsp;</p><p>&nbsp;SLES: 12+&nbsp;</p><p>&nbsp;macOS: 10.13+&nbsp;</p><p>&nbsp;Windows Client: 7, 8.1, 10 (1607+)&nbsp;</p><p>&nbsp;Windows Server: 2012 R2 SP1+&nbsp;</p><p>&nbsp;注意：Windows窗体和WPF应用程序仅在Windows上运行。&nbsp;</p><p>&nbsp;Chip support follows:&nbsp;</p><p>&nbsp;Windows，macOS和Linux上的x64&nbsp;</p><p>&nbsp;Windows上的x86&nbsp;</p><p>&nbsp;Windows和Linux上的ARM32&nbsp;</p><p>&nbsp;Linux上的ARM64（内核4.14+）&nbsp;</p><p>注意：请确保.NET Core 3.0 ARM64部署使用Linux内核4.14版本或更高版本。例如，Ubuntu 18.04满足此要求，但16.04不满足。</p><blockquote><span style=\"font-size: large;\">.NET Core 3.0 部分亮点</span></blockquote><p><span style=\"color: rgb(0, 0, 0); font-size: medium; font-weight: bold;\">1.WPF和Windows Form</span></p><p>现在，可以在Windows上使用.NET Core 3.0构建WPF和Windows Forms应用程序。Visual Studio 2019 16.3支持创建面向.NET Core的WPF应用程序。这包括新模板以及更新后的XAML设计器和XAML Hot Reload。</p><p>下图显示了在新设计器中显示的WPF应用程序：<br></p><p><img style=\"max-width:100%;\" src=\"//blog-1252786999.cos.ap-chengdu.myqcloud.com/images/upload/20190925_268666.png\"></p><p>Windows Forms设计器仍处于预览状态，可以<a href=\"https://aka.ms/winforms-designer\" target=\"_blank\" style=\"color: rgb(49, 148, 208); text-decoration-line: underline;\">单独下载获得</a>。</p><p><span style=\"font-size: medium; font-weight: bold; color: rgb(0, 0, 0);\">2.可空引用类型</span></p><p>C# 8.0引入了可为空的引用类型。</p><p><span style=\"color: rgb(0, 0, 0); font-weight: bold; font-size: medium;\">3.接口成员的默认实现</span></p><p>C# 8.0可以为接口成员提供默认实现。如果实现该接口的类没有实现该成员，那么将调用改成员的默认实现。</p>在此示例中，ConsoleLogger类不必实现ILogger接口成员的重载Log(Exception)方法，因为它是使用默认实现声明的。现在，只要为现有实现者提供默认实现，就可以将其添加到现有的公共接口中。</p><p><span style = \"font-weight: bold; font-size: medium; color: rgb(0, 0, 0);\" > 4.异步流 </span ></p>",Cover="//blog-1252786999.cos.ap-chengdu.myqcloud.com/images/cover/20190924_130381.png",CreateTime=DateTime.Now,IsRecommend=false,IsTop=true,ReadCount=1467,RemarkCount=0,Status=1,Title=".NET Core 3.0正式版发布",AllowRemark=true,AllowReply=true,PraiseCount=15,ShareCount=1,Keywords="Core 3.0,C# 8.0,.NET" }
            };

            var announcements = new Announcement[] {
                new Announcement{ Id=1,Content="后台程序已正式升级为.NET Core 3.0。",CreateTime=DateTime.Now,Level=1,Status=1 },
                new Announcement{ Id=2,Content="全站正式升级为HTTPS",CreateTime=DateTime.Now,Level=2,Status=1 }
            };

            var carousel = new Carousel[] {
                new Carousel{ Id=1,CreateTime=DateTime.Now,ImgUrl="//blog-1252786999.cos.ap-chengdu.myqcloud.com/images/carousel/20180417_763073.jpg",Link="https://github.com/leo0216/winadmin",Status=1,Title="个人作品《WinAdmin》登场",Order=1,Target=Enum.Target._blank },
                new Carousel{ Id=2,CreateTime=DateTime.Now,ImgUrl="//blog-1252786999.cos.ap-chengdu.myqcloud.com/images/carousel/20191008_495806.png",Link="https://github.com/Leo0216/Blog2",Status=1,Title="不落阁2.0前端正式开源",Order=0,Target=Enum.Target._blank }
            };

            var friendLink = new FriendLink[] {
                new FriendLink{ Id=1,CreateTime=DateTime.Now,Domain="yanshisan.cn",Favicon="https://www.yanshisan.cn/logo.png",Link="https://www.yanshisan.cn",Status=1,Title="燕十三" },
                new FriendLink{ Id=2,CreateTime=DateTime.Now,Domain="xgblack.cn",Favicon="https://blog.xgblack.cn/favicon.ico",Link="https://blog.xgblack.cn",Status=1,Title="臾离" }
            };

            var note = new Note[] {
                new Note{ Id=1,Content="int totalPage = (totalCount  +  pageSize  - 1) / pageSize; ",CreateTime = DateTime.Now,Status=1,Tag="分页",Title="分页获取总页数" },
                new Note{ Id=2,Content="Regex.Replace(手机号, \"(\\d{3})\\d{ 4} (\\d{ 4})\", \"$1 * ***$2\")",CreateTime = DateTime.Now,Status=1,Tag= "正则、手机号",Title = "C#手机号隐藏中间四位" }
            };

            var production = new Production[] {
                new Production{ Id=1,Cover="//blog-1252786999.cos.ap-chengdu.myqcloud.com/images/cover/20190412_697622.jpg",Intro="经典博客前端模板",CreateTime=DateTime.Now,Link="https://github.com/Leo0216/BlogTemplate",Status=1,Title="不落阁1.0博客模板",DownloadUrl="https://github.com/Leo0216/BlogTemplate/archive/master.zip" }
            };

            var timeline = new Timeline[] {
                new Timeline{ Id=1,Content="采用第三方制作的时光轴上线啦",CreateTime =DateTime.Now,Status=1,Title="采用第三方制作的时光轴上线啦" },
                new Timeline{ Id=2,Content="浮世三千，吾爱有三。 日、月与卿。 日为朝，月为暮， 卿为朝朝暮暮。",CreateTime =DateTime.Now,Status=1,Title="中文翻译" },
                new Timeline{ Id=3,Content="l love three things in this world. Sun,Moon and You. Sun for morning,Moon for night,and You forever.",CreateTime =DateTime.Now,Status=1,Title="英语老师的情书" }
            };

            //modelBuilder.Entity<ArticleCategory>().HasData(articleCategorys);
            //modelBuilder.Entity<Article>().HasData(articles);
            //modelBuilder.Entity<Announcement>().HasData(announcements);
            //modelBuilder.Entity<Carousel>().HasData(carousel);
            //modelBuilder.Entity<FriendLink>().HasData(friendLink);
            //modelBuilder.Entity<Note>().HasData(note);
            //modelBuilder.Entity<Production>().HasData(production);
            //modelBuilder.Entity<Timeline>().HasData(timeline);
            #endregion

            modelBuilder.Entity<SystemLog>();
            modelBuilder.Entity<RemarkReply>().HasOne(s => s.User).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RemarkReply>().HasOne(s => s.TargetUser).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Article>().HasOne(s => s.Attachment).WithMany(s => s.Articles).OnDelete(DeleteBehavior.ClientSetNull);
            modelBuilder.Entity<CommentReply>().HasOne(s => s.User).WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<CommentReply>().HasOne(s => s.TargetUser).WithMany().OnDelete(DeleteBehavior.Restrict);

            #region 角色菜单多对多
            modelBuilder.Entity<RoleMenu>().HasKey(s => new { s.RoleId, s.MenuId });
            modelBuilder.Entity<RoleMenu>().HasOne(s => s.Role).WithMany(s => s.RoleMenus).HasForeignKey(s => s.RoleId);
            modelBuilder.Entity<RoleMenu>().HasOne(s => s.Menu).WithMany(s => s.RoleMenus).HasForeignKey(s => s.MenuId);
            #endregion

            #region 文章音乐多对多
            modelBuilder.Entity<ArticleMusic>().HasKey(s => new { s.ArticleId, s.MusicId });
            modelBuilder.Entity<ArticleMusic>().HasOne(s => s.Music).WithMany(s => s.ArticleMusics).HasForeignKey(s => s.MusicId);
            modelBuilder.Entity<ArticleMusic>().HasOne(s => s.Article).WithMany(s => s.ArticleMusics).HasForeignKey(s => s.ArticleId);
            #endregion


            base.OnModelCreating(modelBuilder);
        }
    }
}
