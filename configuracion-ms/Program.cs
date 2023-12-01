using Infrastructure.Repositories;

namespace configuracion_ms
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add service to CORS
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("*")
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Using MongoDB in collections
            builder.Services.Configure<BaseRepository>(
            builder.Configuration.GetSection("ConfiguracionMS"));
            builder.Services.AddSingleton<CampaignGuideRepository>();
            builder.Services.AddSingleton<AgentRepository>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseCors();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
