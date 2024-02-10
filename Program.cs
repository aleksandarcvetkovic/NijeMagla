using Microsoft.Extensions.Options;
using MongoDB.Driver;
namespace Nije_Magla_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.Configure<NijeMaglaDatabaseSettings>(
                builder.Configuration.GetSection(nameof(NijeMaglaDatabaseSettings)));

            builder.Services.AddSingleton<INijeMaglaDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<NijeMaglaDatabaseSettings>>().Value);

            builder.Services.AddSingleton<IMongoClient>(s =>
                    new MongoClient(builder.Configuration.GetValue<string>("NijeMaglaDatabaseSettings:ConnectionString")));

            builder.Services.AddScoped<INijeMaglaService, NijeMaglaService>();



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
