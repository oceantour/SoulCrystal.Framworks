using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using NLog;
using NLog.Config;
using NLog.LayoutRenderers;
using NLog.Web;
using NLog.Web.LayoutRenderers;

namespace Microsoft.Extensions.Hosting
{
    /// <summary> NLog 帮助类 </summary>
    internal static class NLogExtensions
    {
        /// <summary> 使用 NLog Web 服务 </summary>
        public static IWebHostBuilder UseNLogWeb(this IWebHostBuilder builder)
        {
            LayoutRenderer.Register<NLogLayoutRenderer>(NLogLayoutRenderer.LayoutRendererName);

            builder.UseNLog();
            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var env = context.HostingEnvironment;
                env.ConfigureNLog($"{env.ContentRootPath}{Path.DirectorySeparatorChar}NLog.config");

                LogManager.Configuration.Variables["configDir"] = env.ContentRootPath;
            });

            return builder;
        }

        /// <summary> 配置日志 </summary>
        /// <param name="env"> 主机环境 </param>
        /// <param name="configFileRelativePath"> 配置文件相对路径 </param>
        /// <returns> 日志配置信息 </returns>
        public static LoggingConfiguration ConfigureNLog(this IHostEnvironment env, string configFileRelativePath)
        {
            var assembly = typeof(NLogExtensions).GetTypeInfo().Assembly;
            var fileName = Path.Combine(env.ContentRootPath, configFileRelativePath);

            ConfigurationItemFactory.Default.RegisterItemsFromAssembly(assembly);

            LogManager.AddHiddenAssembly(assembly);
            LogManager.LoadConfiguration(fileName);

            return LogManager.Configuration;
        }

        /// <summary> 日志模板渲染器 </summary>
        [LayoutRenderer(LayoutRendererName)]
        public class NLogLayoutRenderer : AspNetLayoutRendererBase
        {
            public const string LayoutRendererName = "siro-benchmarks-web";

            protected override void DoAppend(StringBuilder builder, LogEventInfo logEvent)
            {
                builder.Append("Srio");
            }
        }
    }
}
