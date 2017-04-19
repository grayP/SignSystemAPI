using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using SignSystem.API.Entities;
using SignSystem.API.Models.Signs;
using SignSystem.API.Services;
using SignSystem.API.Services.Signs;
using SignSystem.API.Services.Stores;

namespace SignSystem.API
{
    public class Startup
    {
        public static IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appSettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddMvcOptions(o => o.OutputFormatters.Add(
                    new XmlDataContractSerializerOutputFormatter()));
            //.AddJsonOptions(o => {
            //    if (o.SerializerSettings.ContractResolver != null)
            //    {
            //        var castedResolver = o.SerializerSettings.ContractResolver
            //            as DefaultContractResolver;
            //        castedResolver.NamingStrategy = null;
            //    }
            //});

#if DEBUG
            services.AddTransient<IMailService, LocalMailService>();
#else
            services.AddTransient<IMailService, CloudMailService>();
#endif
            var connectionString = Startup.Configuration["connectionStrings:signSystemDBConnectionStringLive"];
            services.AddDbContext<SignSystemInfoContext>(o => o.UseSqlServer(connectionString));

            services.AddScoped<IStoreRepository, StoreRepository>();
            services.AddScoped<ISignRepository, SignRepository>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            SignSystemInfoContext signSystemInfoContext)
        {
            loggerFactory.AddConsole();

            loggerFactory.AddDebug();

            //loggerFactory.AddProvider(new NLog.Extensions.Logging.NLogLoggerProvider());
            loggerFactory.AddNLog();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

         //   signSystemInfoContext.EnsureSeedDataForContext();

            app.UseStatusCodePages();

            AutoMapper.Mapper.Initialize(cfg =>
            {
                //cfg.CreateMap<Entities.City, SignDto>();
                //cfg.CreateMap<Entities.City, Models.CityDto>();
                //cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
                //cfg.CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
                //cfg.CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
                //cfg.CreateMap<Entities.PointOfInterest, Models.PointOfInterestForUpdateDto>();
                cfg.CreateMap<Entities.Sign, SignDto>();
                cfg.CreateMap<Entities.Store, Models.Stores.StoreDto>();
            });

            app.UseMvc();

            //app.Run((context) =>
            //{
            //    throw new Exception("Example exception");
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
