using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using core.configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Candidate_Management.CORE.LoadingTemplates;
using Candidate_Management.CORE.Remind;
using Candidate_Management.CORE.Loading;
using System.Collections;
/*
    Author : ZAAFRANI Gabriel
    Version : 1.0
 */
namespace ConsoleApplication
{
    public class Program
    {
        public static JsonConfiguration jsonConf;

          public static ILoggerFactory loggerFactory;
               // This method gets called by the runtime. Use this method to add services to the container.


          public void Configure(IApplicationBuilder app)
          {
            // ici nous allons ouvrire le fichier de configuration
           
           
            
            app.UseMvcWithDefaultRoute();
             // ********************
            // USE CORS - might not be required.
            // ********************
            app.UseCors("SiteCorsPolicy");
            
         }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
             // ********************
            // Setup CORS
            // ********************
            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin();
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });
            
        }

     
        public static void Main(string[] args)
        {    
            try{
                Context loadingTheFolders = new Context();
                loadingTheFolders.setFolders(new LoadingEmailTemplate());
                loadingTheFolders.setFolders(new LoadingPlugins());
                loadingTheFolders.executeLoading();
                var host = new WebHostBuilder()
                            .UseKestrel()
                            .UseUrls(JsonConfiguration.getInstance().getUrlOrIpAdressWithPort())
                            .UseStartup<Startup>()
                            .Build();
                host.Run();
            }catch(Exception exc){
                Console.WriteLine($"Une erreur {exc.Message} c'est produit, veuillez consulter vos logs");
            }
                
        }
}
}

