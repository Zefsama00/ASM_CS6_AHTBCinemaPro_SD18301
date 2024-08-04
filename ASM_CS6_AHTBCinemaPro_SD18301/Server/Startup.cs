using ASM_CS6_AHTBCinemaPro_SD18301.Data;
using ASM_CS6_AHTBCinemaPro_SD18301.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text.Json.Serialization;
using ASM_CS6_AHTBCinemaPro_SD18301.Server.Data;

namespace ASM_CS6_AHTBCinemaPro_SD18301.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DBCinemaContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DBConnection")));

            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.MaxDepth = 64;
            });

            services.AddControllersWithViews();
            services.AddRazorPages();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            services.AddHttpClient();

           
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseWebAssemblyDebugging();
                // Remove or comment out this line
                // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseDeveloperExceptionPage();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
