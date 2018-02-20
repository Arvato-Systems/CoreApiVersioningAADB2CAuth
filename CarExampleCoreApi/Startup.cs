using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using CarExampleCoreApi.Security;
using Swashbuckle.AspNetCore.Swagger;

namespace CarExampleCoreApi
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
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddAzureAdB2CBearer(options => Configuration.Bind("AzureAdB2C", options));

            services.AddAuthorization(options =>
            {
                //options.AddPolicy("Admin", policy => policy.RequireClaim("extension_AppRole", "Administrator"));
                options.AddPolicy("Admin", policy => policy.AddRequirements(new MailDomainRequirement("@bertelsmann.de")));
                //options.AddPolicy(
                //    "Admin",
                //    policyBuilder => policyBuilder.RequireAssertion(
                //        context => context.User.HasClaim(claim => claim.Type == "emails")
                //        && context.User.FindFirstValue("emails").ToLower().Contains("@bertelsmann.de")
                //    )
                //);
            });
            services.AddSingleton<IAuthorizationHandler, MailDomainHandler>();
            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "CAR API", Version = "v1", Description = "Simple API for Cars", Contact = new Contact { Name = "Thomas Zühlke", Email = "thomas.zuehlke@bertelsmann.de" } });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CAR API V1");
            });

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
