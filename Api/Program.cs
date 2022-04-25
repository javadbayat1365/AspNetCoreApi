using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Sentry;

namespace Api
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            try
            {
                logger.Debug("شروع برنامه");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception w)
            {

                logger.Error(w,"برنامه در هنگام بالا آمدن با خطا مواجه شده است");
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
            }
            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            //Delete Defualt Provider .net Core
            .ConfigureLogging(options => options.ClearProviders())
            //Use NLog 
            .UseNLog()
                .UseStartup<Startup>();
    }
}
