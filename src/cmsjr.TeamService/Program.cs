using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace cmsjr.TeamService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder().AddCommandLine(args).Build();
            var argParts = args[0].Split("=");
    
            var host = WebHost.CreateDefaultBuilder(args)
                
                .UseKestrel()
                .UseUrls(argParts[1])
                .UseStartup<Startup>()
                .ConfigureServices(services =>{})
                .UseConfiguration(config)
                .Build();
            
            host.Run();
        }

        
           
    }
}