using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TextMicroservice
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // creates a host, builds it, and runs it
            CreateHostBuilder(args).Build().Run();

        }

        //responsible for configuring the host and setting up the web server
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TextMicroservice.Startup>();
                });
    }
}

