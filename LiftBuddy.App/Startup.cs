using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LiftBuddy.App.Data;
using LiftBuddy.App.Repositories;
using LiftBuddy.App.Services;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using Radzen;

namespace LiftBuddy.App
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Blazor setup
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<WeatherForecastService>();
            
            // Mongo configuration
            var conventionPack = new  ConventionPack {new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);
            
            // Mongo client
            var mongoDbConnectionString = _configuration["ConnectionStrings:Mongo"];
            services.AddScoped<IMongoClient, MongoClient>(_ => new MongoClient(MongoClientSettings.FromConnectionString(mongoDbConnectionString)));
            
            // Radzen components
            services.AddScoped<DialogService>();
            services.AddScoped<NotificationService>();
            services.AddScoped<TooltipService>();
            services.AddScoped<ContextMenuService>();

            // Repositories
            services.AddScoped<WorkoutRepository>();

            // Services
            services.AddScoped<WorkoutService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}