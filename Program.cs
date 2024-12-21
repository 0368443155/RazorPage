using CS58_Razor09_Entity_ASP.Models;
using Microsoft.EntityFrameworkCore;

namespace CS58_Razor09_Entity_ASP
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //Get services
            var services = builder.Services;
            // Add services to the container.
            services.AddRazorPages();
            services.AddDbContext<MyBlogContext>(options =>
            {
                string connectString = builder.Configuration.GetConnectionString("MyBlogContext");
                options.UseSqlServer(connectString);
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
//tự động phát sinh CRUD theo model
//dotnet aspnet-codegenerator razorpage -m CS58_Razor09_Entity_ASP.Models.Article -dc CS58_Razor09_Entity_ASP.Models.MyBlogContext -outDir Pages/Blog -udl --referenceScriptLibraries
