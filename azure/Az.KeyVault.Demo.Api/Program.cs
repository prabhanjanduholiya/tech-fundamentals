using Az.KeyVault.Demo.Api;
using Azure.Extensions.AspNetCore.Configuration.Secrets;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography.X509Certificates;

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

        if (builder.Environment.IsProduction())
        {
            var vaultName = builder.Configuration["KeyVaultName"];

            var secretClient = new SecretClient(
                new Uri($"https://{vaultName}.vault.azure.net/"),
                new DefaultAzureCredential());

            var opt = new AzureKeyVaultConfigurationOptions()
            {
                // ReloadInterval = TimeSpan.FromSeconds(1),
                Manager = new KeyVaultSecretManager()
            };

            builder.Configuration.AddAzureKeyVault(secretClient, opt);
        }

        builder.Services.Configure<PositionOptions>(
    builder.Configuration.GetSection(PositionOptions.Position));

        var app = builder.Build();

        //app.Configuration.GetReloadToken();

        app.MapGet("/configuration/reload", () => ReloadConfifuration(builder));

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

    private static string ReloadConfifuration(WebApplicationBuilder builder)
    {
        var configRoot = builder.Configuration as IConfigurationRoot;

        if (configRoot != null)
        {
            configRoot.Reload();
            return "Application ConfigurationSetting Refreshed...!";
        }
        else
        {
            return "Application ConfigurationSetting Not Refreshed...!";
        }
    }
}