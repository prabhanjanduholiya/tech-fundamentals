using duholiya.communication.service.data;
using duholiya.communication.service.data.Contracts;
using duholiya.communication.service.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        AddApplicationDependencies(builder);

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

        static void AddApplicationDependencies(WebApplicationBuilder builder)
        {
            //builder.Services.AddServiceBus();
            builder.Services.AddScoped<QueueingService>();
            builder.Services.AddScoped<IOtpRepository, OtpRepository>();
        }
    }
}