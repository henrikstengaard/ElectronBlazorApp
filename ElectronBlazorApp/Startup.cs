using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ElectronBlazorApp.Data;
using ElectronNET.API;
using ElectronNET.API.Entities;
using MudBlazor.Services;

namespace ElectronBlazorApp
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMudServices();
            services.AddMudBlazorDialog();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<AppState>();
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
            
            if (HybridSupport.IsElectronActive)  
            {
                Task.Run(async () =>
                {
                    await CreateWindow();
                });
            }
        }
        
        private async Task CreateWindow()
        {
            var iconPath = Path.Combine(_env.WebRootPath, "favicon.ico");
            Console.WriteLine($"icon = '{iconPath}'");
            var window = await Electron.WindowManager.CreateWindowAsync(new BrowserWindowOptions
            {
                Width = 1074,
                Height = 768,
                Center = true,
                BackgroundColor = "#FFFFFF",
                Frame = false,                
                WebPreferences = new WebPreferences
                {
                    NodeIntegration = true
                },
                Icon = iconPath
            });
            window.OnClosed += () => {  
                Electron.App.Quit();  
            };
        } 
    }
}