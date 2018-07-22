using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Bot.Connector;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StatusBot.DataSources;
using StatusBot.Services;

namespace StatusBot
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
            /*
             * Bot Framework authentication configuration 
             */

            var msAppIdKey = Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppIdKey)?.Value;
            var msAppPwdKey = Configuration.GetSection(MicrosoftAppCredentials.MicrosoftAppPasswordKey)?.Value;

            services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddBotAuthentication(msAppIdKey, msAppPwdKey);

            services.AddMvc(o =>
            {
                o.Filters.Add(typeof(TrustServiceUrlAttribute));
            });

            /*
             * IOC configuration 
             */
            services.AddTransient<IMessageService, MessageService>();
            services.AddTransient<IGitHubRepository, GitHubRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
