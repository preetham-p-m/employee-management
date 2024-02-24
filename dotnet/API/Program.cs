using API.Extension;
using Microsoft.EntityFrameworkCore;
using Persistance;

namespace API
{
    public class Program
    {
        protected Program() { }

        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddServices();
            builder.Services.AddValidators();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder
                .Services.AddEntityFrameworkNpgsql()
                .AddDbContext<DataContext>(opt =>
                {
                    // var connectionStrings = builder.Configuration.GetConnectionString("Default");
                    opt.UseNpgsql(
                        "User ID =postgres;Password=admin;Server=localhost;Port=5432;Database=EmployeeManagement; Integrated Security=true;Pooling=true;"
                    );
                });

            var app = builder.Build();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            using var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
            }
            catch (System.Exception)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Error during Migration");
                throw;
            }

            app.Run();
        }
    }
}
