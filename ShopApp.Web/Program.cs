using ShopApp.Application.Service.CategoriaService;
using ShopApp.Percistence.Repositories.Categoria;


namespace ShopApp.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.Scan(scan => scan
            .FromAssemblies(typeof(CategoriaService).Assembly)
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Service")))
            .AsSelf()
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            
            .FromAssemblies(typeof(CategoriaRepository).Assembly)
            .AddClasses(c => c.Where(t => t.Name.EndsWith("Repository")))
            .AsSelf()
            .AsImplementedInterfaces()
            .WithScopedLifetime()
            );

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
