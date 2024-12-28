using Az.Samples.BlobStorage.Lib.Contracts;
using Azure.Storage.Blobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Az.Samples.BlobStorage.Lib
{
    public static class BlobStorageExtensions
    {
        public static IServiceCollection AddAzureBlobStorageServices(this IServiceCollection services, string connectionString)
        {
            services.AddScoped(_ => new BlobServiceClient(connectionString));

            services.AddScoped<IBlobContainerManager, BlobContainerManager>();
            
            services.AddScoped<IBlobManager, BlobManager>();
            
            return services;
        }
    }
}