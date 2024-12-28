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

namespace Az.Samples.BlobStorage.Lib.Contracts
{
    public interface IBlobManager
    {
        /// <summary>
        /// Get Blob SAS Uri
        /// </summary>
        /// <param name="container">Container name</param>
        /// <param name="blobName">The blob name</param>
        /// <param name="expiresOn">TimeSpan</param>
        /// <returns>Blob SAS uri</returns>
        Task<string> GetBlobSasUri(string container, string blobName, TimeSpan expiresOn);

        /// <summary>
        /// Deletes a blob
        /// </summary>
        /// <param name="blobName">The name of the blob to delete</param>
        /// <param name="deleteSnapshotOption">The set of options describing delete operation.</param>
        /// <returns>True, if the blob was deleted or did not exist, otherwise, false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the "blobName" parameter is null or empty.</exception>
        Task<bool> DeleteAsync(string container, string blobName,
           DeleteSnapshotsOption deleteSnapshotOption = DeleteSnapshotsOption.IncludeSnapshots);

        /// <summary>
        /// Restores a previously deleted the blob
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <returns>True, upon successful restoration, otherwise,false.</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty.</exception>
        Task<bool> UndeleteAsync(string container, string blobName);

        /// <summary>
        /// Downloads a blob from the service, including its metadata and properties.
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <returns>A BlobDownloadInfo object</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty.</exception>
        //Task<BlobDownloadInfo> DownloadAsync(string container, string blobName);

        /// <summary>
        /// Downloads the blob to the specified stream
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="destinationStream">The stream to send the blob to</param>
        /// <returns>True, if successful,otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the destinationStream is null</exception>
        Task<bool> DownloadToAsync(string container, string blobName, Stream destinationStream);

        /// <summary>
        /// Downloads the blob to the specified file
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="path">The full filename to send the blob to</param>
        /// <returns>True, if successful,otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName or path is null or empty</exception>
        Task<bool> DownloadToAsync(string container, string blobName, string path);

        /// <summary>
        /// Indicates if the blob exists
        /// </summary>
        /// <param name="blobName">The blob name</param>
        /// <returns>True if the blob exists, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty</exception>
        Task<bool> ExistsAsync(string container, string blobName);

        /// <summary>
        /// Uploads the stream to the container
        /// </summary>
        /// <param name="blobName">The name of the blob</param>
        /// <param name="sourceStream">The source stream</param>
        /// <param name="overwriteIfExists">Indicates if the blob should be overwritten if it exists.</param>
        /// <returns>The detail around the blob</returns>
        /// <exception cref="ArgumentNullException">Throws if the blobName is null or empty or the sourceStream is null</exception>
        Task<BlobContentInfo> UploadAsync(string container, string blobName, Stream sourceStream, bool overwriteIfExists = false);
    }
}
