
using APIWithFireBase.Models;
using Integrate.NetAPIWithFirebase.Models;
using Microsoft.EntityFrameworkCore;

namespace Integrate.NetAPIWithFirebase
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddHttpClient();
            builder.Services.Configure<FirebaseSettings>(builder.Configuration.GetSection("FirebaseSettings"));

    //        builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    //options.UseSqlServer("Your_Connection_String"));


            builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("Local");
                options.UseSqlServer(connectionString);
            });

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped<FirebaseService, FirebaseService>();

            builder.Services.AddScoped<FirebaseSyncService>();

            //var app = builder.Build();
            //----------------------------FireBase-------------------
            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var syncService = scope.ServiceProvider.GetRequiredService<FirebaseSyncService>();
                syncService.StartListeningAsync();
            }

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
