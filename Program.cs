using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using core.configuration;
using core.plugin.engine;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Candidate_Management.CORE.LoadingTemplates;
using Candidate_Management.CORE.Remind;
using scheduler;
using Candidate_Management.CORE.LoadingPlugin;

using Candidate_Management.CORE.Remind;

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

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        

        public static void Main(string[] args)
        {
            /*
            string date = DateTime.Today.AddDays(1).ToString("MM/dd/yy",CultureInfo.InvariantCulture);
            string cmd = $"script.sh {date}";
            Console.WriteLine(cmd);
            Schedule schedule = new Schedule(cmd);
            schedule.executeTask();  
             */
            
            try{
                appellerRemind remind = new appellerRemind();
                // (int idJob,string fileName,string candidateName,string Candidatefirstname, DateTime meeting)
                remind.id = 2;
                remind.exec("23",DateTime.Now);
                JsonConfiguration conf = JsonConfiguration.getInstance();
                string folder = conf.getPluginFolder();
                LoadPlugins load = new LoadPlugins(folder);
                load.getPluginFromFolders();
                Console.WriteLine($"The folder is {folder}");
                Context contexte = new Context(new LoadingEmailTemplate());
                contexte.executeLoading();
                var host = new WebHostBuilder()
                            .UseKestrel()
                            //.UseUrls("http://192.168.1.31:5000")
                            .UseStartup<Startup>()
                            .Build();
                host.Run();
            }catch(Exception exc){
                Console.WriteLine($"Une erreur {exc.Message} c'est produit, veuillez consulter vos logs");
            }
                
        }
}
}

