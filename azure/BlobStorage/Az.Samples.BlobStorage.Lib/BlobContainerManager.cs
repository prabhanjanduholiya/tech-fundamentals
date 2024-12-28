using Az.Samples.BlobStorage.Lib.Contracts;
using Az.Samples.BlobStorage.Lib.Exceptions;
using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Az.Samples.BlobStorage.Lib
{
    internal class BlobContainerManager : IBlobContainerManager
    {
        private readonly BlobServiceClient _blobServiceClient;

        public BlobContainerManager(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public async Task<BlobContainerClient> CreateBlobContainerAsync(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException(nameof(containerName), "The container name cannot be null or empty");
            }

            try
            {
                Response<BlobContainerClient> apiResponse = await _blobServiceClient.CreateBlobContainerAsync(containerName);

                return apiResponse.Value;
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.ContainerAlreadyExists)
            {
                throw new RecordAlreadExistsException(containerName);
            }
        }


        /// <summary>
        /// Deletes the container
        /// </summary>
        /// <param name="containerName">The name of the container</param>
        /// <returns>True, if successful, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        public async Task<bool> DeleteContainerAsync(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException(nameof(containerName), "The container name cannot be null or empty");
            }

            try
            {
                Response apiResponse = await _blobServiceClient.DeleteBlobContainerAsync(containerName);

                return apiResponse.Status is ((int)HttpStatusCode.NoContent) or ((int)HttpStatusCode.Accepted);
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.ContainerBeingDeleted ||
                      ex.ErrorCode == BlobErrorCode.ContainerNotFound)
            {
                // Ignore any errors if the blob is being deleted or not found
                return true;
            }
        }

        /// <summary>
        /// Returns a reference to the container
        /// </summary>
        /// <param name="containerName">The container name</param>
        /// <returns>A reference to the container, if successful</returns>
        /// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        public BlobContainerClient GetBlobContainerClient(string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException(nameof(containerName), "The container name cannot be null or empty");
            }

            BlobContainerClient blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

            return blobContainerClient;
        }

        /// <summary>
        /// Returns a list of all of the containers in the current storage account
        /// </summary>
        /// <returns>A List of BlobContainerItems</returns>
        public async Task<IList<BlobContainerItem>> GetAllContainersAsync()
        {
            AsyncPageable<BlobContainerItem> apiResponse = _blobServiceClient.GetBlobContainersAsync();
            IAsyncEnumerator<BlobContainerItem> enumerator = apiResponse.GetAsyncEnumerator();
            List<BlobContainerItem> blobContainerItems = new();

            try
            {
                while (await enumerator.MoveNextAsync())
                {
                    BlobContainerItem blobContainerItem = enumerator.Current;
                    blobContainerItems.Add(blobContainerItem);
                }
            }
            finally
            {
                await enumerator.DisposeAsync();
            }

            return blobContainerItems;
        }
    }
}
