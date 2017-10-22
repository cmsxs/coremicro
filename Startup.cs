using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace hwapp
{
    public class Startup
    {
        public Startup(IHostingEnvironment hostingEnvironment)
        {
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.Run(async (context) => { await context.Response.WriteAsync("Hello, world!!"); });
        }
    }
}