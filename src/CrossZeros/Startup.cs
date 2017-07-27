using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Serialization;
using CrossZeros.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using CrossZeros.ViewModels;

namespace CrossZeros
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
              .SetBasePath(env.ContentRootPath)
              .AddJsonFile("config.json")
              .AddJsonFile($"config.{env.EnvironmentName}.json", optional: true)
              .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(config =>
            {
#if !DEBUG
                config.Filters.Add(new RequireHttpsAttribute());
#endif
            })
               .AddJsonOptions(opt =>
               {
                   opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
               });


            // получаем строку подключения из файла конфигурации
            string connection = Configuration.GetConnectionString("DefaultConnecton");

            System.Console.WriteLine("Hello" + connection);
            // добавляем контекст MobileContext в качестве сервиса в приложение
            services.AddDbContext<CrossZeroContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));
 
            services.AddIdentity<CrossZeroUser, IdentityRole>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequiredLength = 4;
            })
            .AddEntityFrameworkStores<CrossZeroContext>();

            services.Configure<IdentityOptions>(options => {
                options.Cookies.ApplicationCookie.CookieName = "SessionAuth";
                options.Cookies.ApplicationCookie.LoginPath = new PathString("/Auth/Login");

            });

            services.AddLogging();
            services.AddScoped<IGameProcessor, MSSQLGameProcessor>();               
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));

            app.UseIdentity();

            app.UseStaticFiles();

            Mapper.Initialize(config =>
            {
                config.CreateMap<Game, GameViewModel>().ReverseMap();
                config.CreateMap<GameState, GameStateViewModel>().ReverseMap();
                
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
