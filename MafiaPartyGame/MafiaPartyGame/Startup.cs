using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MafiaPartyGame.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MafiaPartyGame
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddSignalR();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors(builder => builder
            .WithOrigins("http://localhost:8080", "http://10.3.19.111:8080", "http://10.3.77.241:8080", "http://10.3.160.167:8080", "http://10.7.145.234:8080", "http://10.7.148.173:8080")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod());


            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<GameHub>("/hub");
            });
        }
    }
}
