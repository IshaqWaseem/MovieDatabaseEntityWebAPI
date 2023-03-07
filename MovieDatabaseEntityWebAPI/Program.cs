using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MovieDatabaseEntityWebAPI.Models;
using MovieDatabaseEntityWebAPI.Services.Characters;
using MovieDatabaseEntityWebAPI.Services.Franchises;
using MovieDatabaseEntityWebAPI.Services.Movies;
using System.Reflection;

namespace MovieDatabaseEntityWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddDbContext<MoviesDbContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
            // Automapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddScoped<IMovieService, MovieService>();
            builder.Services.AddScoped<IFranchiseService,FranchiseService>();
            builder.Services.AddScoped<ICharacterService,CharacterService>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Postgrad API",
                    Description = "Simple API to manage postgraduate studies",
                    Contact = new OpenApiContact
                    {
                        Name = "Ishaq Waseem",
                        Url = new Uri("https://github.com/IshaqWaseem")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT 2022",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });
                options.IncludeXmlComments(xmlPath);
            }
);

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