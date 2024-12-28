using Az.Samples.BlobStorage.Lib.Contracts;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Az.Samples.BlobStorage.Demo.Controllers
{
    [Route("api/blob-storage/{container}/blobs")]
    [ApiController]
    public class BlobController : ControllerBase
    {
        private readonly ILogger<BlobController> _logger;

        private readonly IBlobManager _blobManager;

        public BlobController(IBlobManager blobManager, ILogger<BlobController> logger)
        {
            _logger = logger;
            _blobManager = blobManager;
        }

        /// <summary>
        /// Uploads the file to the container
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="filename">The fully qualified file name</param>
        /// <param name="overwriteIfExists">Indicates if the blob should be overwritten if it exists.</param>
        /// <returns>The detail around the blob</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName or filename is null or empty</exception>
        [HttpPost("upload")]
        public async Task<BlobContentInfo> UploadAsync(string container, IFormFile file, bool overwriteIfExists = false)
        {
            string fileName = file.FileName;
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                memoryStream.Position = 0;
                return await _blobManager.UploadAsync(container, fileName, memoryStream, overwriteIfExists);
            }
        }

        /// <summary>
        /// Get Blob SAS Uri
        /// </summary>
        /// <param name="container">Container name</param>
        /// <param name="blobName">The blob name</param>
        /// <param name="expiresOn">TimeSpan</param>
        /// <returns>Blob SAS uri</returns>
        [HttpGet("sas/uri")]
        public Task<string> GetBlobSasUri(string container, string blobName, TimeSpan expiresOn)
        {
            return _blobManager.GetBlobSasUri(container, blobName, expiresOn);
        }

        /// <summary>
        /// Deletes a blob
        /// </summary>
        /// <param name="blobName">The name of the blob to delete</param>
        /// <param name="deleteSnapshotOption">The set of options describing delete operation.</param>
        /// <returns>True, if the blob was deleted or did not exist, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the "blobName" parameter is null or empty.</exception>
        [HttpDelete]
        public async Task<bool> DeleteAsync(string container, string blobName,
             DeleteSnapshotsOption deleteSnapshotOption = DeleteSnapshotsOption.IncludeSnapshots)
        {
            return await _blobManager.DeleteAsync(container, blobName, deleteSnapshotOption);
        }

        /// <summary>
        /// Restores a previously deleted the blob
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <returns>True, upon successful restoration, otherwise,false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty.</exception>
        [HttpPost("undelete")]
        public async Task<bool> UndeleteAsync(string container, string blobName)
        {
            return await _blobManager.UndeleteAsync(container, blobName);
        }

        ///// <summary>
        ///// Downloads a blob from the service, including its metadata and properties.
        ///// </summary>
        ///// <param name="blobName">The name of the blob</param>
        ///// <returns>A BlobDownloadInfo object</returns>
        ///// <exception cref="ArgumentNullException">Throws if the blobName is null or empty.</exception>
        //[HttpGet("download")]
        //public async Task<BlobDownloadInfo> DownloadAsync(string container, string blobName)
        //{
        //    return await _blobManager.DownloadAsync(container, blobName);
        //}

        ///// <summary>
        ///// Downloads the blob to the specified stream
        ///// </summary>
        ///// <param name="blobName">The name of the blob</param>
        ///// <param name="destinationStream">The stream to send the blob to</param>
        ///// <returns>True, if successful,otherwise, false</returns>
        ///// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the destinationStream is null</exception>
        //[HttpGet("download/stream")]
        //public async Task<bool> DownloadToAsync(string container, string blobName, Stream destinationStream)
        //{
        //    return await _blobManager.DownloadToAsync(container, blobName, destinationStream);
        //}

        /// <summary>
        /// Downloads the blob to the specified file
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="path">The full filename to send the blob to</param>
        /// <returns>True, if successful,otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName or path is null or empty</exception>
        [HttpGet("download")]
        public async Task<bool> DownloadToAsync(string container, string blobName, string path)
        {
            return await _blobManager.DownloadToAsync(container.ToString(), blobName, path);
        }

        /// <summary>
        /// Indicates if the blob exists
        /// </summary>
        /// <param name="blobName">The blob name</param>
        /// <returns>True if the blob exists, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty</exception>
        [HttpGet("exists")]
        public async Task<bool> ExistsAsync(string container, string blobName)
        {
            return await _blobManager.ExistsAsync(container, blobName);
        }

        ///// <summary>
        ///// Uploads the stream to the container
        ///// </summary>
        ///// <param name="blobName">The name of the blob</param>
        ///// <param name="sourceStream">The source stream</param>
        ///// <param name="overwriteIfExists">Indicates if the blob should be overwritten if it exists.</param>
        ///// <returns>The detail around the blob</returns>
        ///// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the sourceStream is null</exception>
        //[HttpPost("upload")]
        //public async Task<BlobContentInfo> UploadAsync(string container, string blobName, Stream sourceStream, bool overwriteIfExists = false)
        //{
        //    return await _blobManager.UploadAsync(container, blobName, sourceStream, overwriteIfExists);
        //}


        

    }
}
