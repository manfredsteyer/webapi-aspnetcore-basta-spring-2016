using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Http;
using Microsoft.Extensions.DependencyInjection;
using FlugDemo.Data;
using Microsoft.AspNet.Mvc.Formatters;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

namespace FlugDemo
{
    // ASP.NET Core 1
    public class Startup
    {
        IConfiguration Configuration;

        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");

            if (env.IsDevelopment())
            {
                // dnu commands install Microsoft.Extensions.SecretManager
                // user-secret set [key] [value]

                builder.AddUserSecrets();
            }

            Configuration = builder.Build();

            var secureQueryString = Configuration["SecureConnectionString"];

            Debug.WriteLine(secureQueryString);

        }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen();
            services.AddSignalR();

            // StructureMap

            services.AddMvc().AddMvcOptions(options => {

                var inFormatters = options.InputFormatters;
                var outFormatters = options.OutputFormatters;



                #region xml
                
                var xmlInput = new XmlDataContractSerializerInputFormatter();
                var xmlOutput = new XmlDataContractSerializerOutputFormatter();

                options.InputFormatters.Add(xmlInput);
                options.OutputFormatters.Add(xmlOutput);
                
                #endregion

            }).AddJsonOptions(options => {

                #region options
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Formatting = Formatting.Indented;
                #endregion

            });

            services.AddTransient<IFlugRepository, FlugEfRepository>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseIISPlatformHandler();

            /*
            app.UseJwtBearerAuthentication(options =>
            {
                options.AutomaticAuthenticate = true;
                options.Authority = "https://steyer-identity-server.azurewebsites.net/identity";
                options.Audience = "https://steyer-identity-server.azurewebsites.net/identity/resources";
            });
            */

            app.UseWebSockets();

            var origins = new[] { "http://localhost:8887" };
            app.UseCors(config => config.WithOrigins(origins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else {
                app.UseExceptionHandler("/home/error");
            }

            app.UseSwaggerGen(); // /swagger
            app.UseSwaggerUi(); // /swagger/ui

            app.UseStaticFiles();

            app.UseSignalR();

            app.UseMvc( routes => {
                routes.MapRoute(
                        "default",
                        "{controller=Home}/{action=Index}/{id?}");
            });
            

        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
