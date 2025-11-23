using ShopApp.Application.Service.CategoriaService;
using ShopApp.Percistence.Repositories.Categoria;


namespace ShopApp.pressent
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

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

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
