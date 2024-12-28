using Az.Samples.BlobStorage.Lib.Contracts;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Az.Samples.BlobStorage.Lib.Exceptions;

namespace Az.Samples.BlobStorage.Lib
{
    internal class BlobManager : IBlobManager
    {
        private readonly IBlobContainerManager _blobContainerManager;

        public BlobManager(IBlobContainerManager blobContainerManager)
        {
            _blobContainerManager = blobContainerManager;
        }

        /// <summary>
        /// Get Blob SAS Uri
        /// </summary>
        /// <param name="container">Container name</param>
        /// <param name="blobName">The blob name</param>
        /// <param name="expiresOn">TimeSpan</param>
        /// <returns>Blob SAS uri</returns>
        public async Task<string> GetBlobSasUri(string container, string blobName, TimeSpan expiresOn)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            var blobClient = blobContainer.GetBlobClient(blobName);

            string response = blobClient.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.Add(expiresOn)).ToString();

            return await Task.FromResult(response);
        }

        /// <summary>
        /// Deletes a blob
        /// </summary>
        /// <param name="blobName">The name of the blob to delete</param>
        /// <param name="deleteSnapshotOption">The set of options describing delete operation.</param>
        /// <returns>True, if the blob was deleted or did not exist, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the "blobName" parameter is null or empty.</exception>
        public async Task<bool> DeleteAsync(string container, string blobName,
            DeleteSnapshotsOption deleteSnapshotOption = DeleteSnapshotsOption.IncludeSnapshots)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            try
            {
                Response apiResponse = await blobContainer.DeleteBlobAsync(blobName, deleteSnapshotOption);

                return apiResponse.Status == (int)HttpStatusCode.Accepted;
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                //Ignore any other errors 
                return true;
            }
        }

        /// <summary>
        /// Restores a previously deleted the blob
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <returns>True, upon successful restoration, otherwise,false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty.</exception>
        public async Task<bool> UndeleteAsync(string container, string blobName)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            BlobClient blobClient = blobContainer.GetBlobClient(blobName);

            Response apiResponse = await blobClient.UndeleteAsync();

            return apiResponse.Status == (int)HttpStatusCode.OK;
        }

        /// <summary>
        /// Downloads the blob to the specified stream
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="destinationStream">The stream to send the blob to</param>
        /// <returns>True, if successful,otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the destinationStream is null</exception>
        public async Task<bool> DownloadToAsync(string container, string blobName, Stream destinationStream)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            if (destinationStream == null)
            {
                throw new ArgumentNullException(nameof(destinationStream), "The stream can not be null");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            try
            {
                BlobClient blobClient = blobContainer.GetBlobClient(blobName);

                Response apiResponse = await blobClient.DownloadToAsync(destinationStream);

                // The status does not matter for DownloadTo and it returns a PartialContent
                // At least with a small file and Azurite
                return apiResponse.Status == (int)HttpStatusCode.PartialContent;
            }
            catch (RequestFailedException ex)
                when (ex.Status == (int)HttpStatusCode.NotFound)
            {
                throw new RecordNotFoundException(blobName);
            }
        }

        /// <summary>
        /// Downloads the blob to the specified file
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="path">The full filename to send the blob to</param>
        /// <returns>True, if successful,otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName or path is null or empty</exception>
        public async Task<bool> DownloadToAsync(string container, string blobName, string path)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentNullException(nameof(path), "The path cannot be null or empty");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            try
            {
                BlobClient blobClient = blobContainer.GetBlobClient(blobName);

                Response apiResponse = await blobClient.DownloadToAsync(path);

                // The status does not matter for DownloadTo and it returns a PartialContent
                // At least with a small file and Azurite
                return apiResponse.Status == (int)HttpStatusCode.PartialContent;
            }
            catch (RequestFailedException ex)
                when (ex.Status == (int)HttpStatusCode.NotFound)
            {
                throw new RecordNotFoundException(blobName);
            }
        }

        /// <summary>
        /// Indicates if the blob exists
        /// </summary>
        /// <param name="blobName">The blob name</param>
        /// <returns>True if the blob exists, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty</exception>
        public async Task<bool> ExistsAsync(string container, string blobName)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            BlobClient blobClient = blobContainer.GetBlobClient(blobName);

            return await blobClient.ExistsAsync();
        }

        /// <summary>
        /// Uploads the stream to the container
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="sourceStream">The source stream</param>
        /// <param name="overwriteIfExists">Indicates if the blob should be overwritten if it exists.</param>
        /// <returns>The detail around the blob</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the sourceStream is null</exception>
        public async Task<BlobContentInfo> UploadAsync(string container, string blobName, Stream sourceStream, bool overwriteIfExists = false)
        {
            if (string.IsNullOrEmpty(container))
            {
                throw new ArgumentNullException(nameof(container), "The container cannot be null or empty");
            }

            if (string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException(nameof(blobName), "The blobName cannot be null or empty");
            }

            if (sourceStream == null)
            {
                throw new ArgumentNullException(nameof(sourceStream), "The source stream can not be null");
            }

            var blobContainer = _blobContainerManager.GetBlobContainerClient(container);

            if (overwriteIfExists)
            {
                BlobClient blobClient = blobContainer.GetBlobClient(blobName);

                return await blobClient.UploadAsync(sourceStream, true);
            }

            return await blobContainer.UploadBlobAsync(blobName, sourceStream);
        }
    }
}
