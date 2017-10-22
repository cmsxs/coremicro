using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
namespace hwapp
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var host = new WebHostBuilder()
                //.UseConfiguration(config)
                .UseKestrel()
                .UseStartup<Startup>()
                .UseUrls("http://0.0.0.0:8080")
                .Build();
            
            host.Run();
        }
    }
}
