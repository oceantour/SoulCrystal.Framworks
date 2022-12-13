using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Siro.Benchmarks.Web
{
    internal class Program
    {
        private static Task Main(string[] args)
        {
            return BuilderHost(args).RunAsync();
        }

        private static IHost BuilderHost(string[] args)
        {
            return Host
                .CreateDefaultBuilder(args)
                .ConfigureLogging(logging => logging.ClearProviders())
                .ConfigureWebHostDefaults(web => web
                    .UseStartup<Startup>()
                    .UseNLogWeb())
                .Build();
        }
    }
}
