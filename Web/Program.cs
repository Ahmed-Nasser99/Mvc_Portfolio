using Core.Interfaces;
using Infrastructure;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace Web
{
    public class Program
    {
   
        public static void Main(string[] args)
        {
     
        var builder = WebApplication.CreateBuilder(args);
  
        // Add services to the container.
        builder.Services.AddControllersWithViews();
            builder.Services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefacultConnection"));
            });
      

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}