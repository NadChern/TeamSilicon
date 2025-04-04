using Blazored.LocalStorage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ContosoCrafts.WebSite.Services;


namespace ContosoCrafts.WebSite
{

    /// <summary>
    /// Configures services and request pipeline for web application.
    /// </summary>
    public class Startup
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration settings for the application.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration settings for the application.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Adds services to the dependency injection container.
        /// This method is called by the runtime.
        /// </summary>
        /// <param name="services">The collection of services to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {

            // This method gets called by the runtime. Use this method to add services to the container.
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddServerSideBlazor();
            services.AddHttpClient();
            services.AddControllers();
            services.AddTransient<JsonFileCategoryService>();
            services.AddTransient<JsonFileFlashcardService>();
            services.AddBlazoredLocalStorage();
            services.AddScoped<LocalStorageFlashcardService>();
            services.AddScoped<LocalStorageCategoryService>();

        }

        /// <summary>
        /// Configures the HTTP request pipeline.
        /// This method is called by the runtime.
        /// </summary>
        /// <param name="app">The application builder to configure the middleware pipeline.</param>
        /// <param name="env">The hosting environment information.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            // Re-execute to a single Razor Page for all status codes
            app.UseStatusCodePagesWithReExecute("/Error", "?statusCode={0}");

            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
            });
        }

    }
}