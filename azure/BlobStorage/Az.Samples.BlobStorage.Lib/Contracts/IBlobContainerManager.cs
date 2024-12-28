using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Az.Samples.BlobStorage.Lib.Contracts
{
    public interface IBlobContainerManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        Task<BlobContainerClient> CreateBlobContainerAsync(string containerName);

        /// <summary>
        /// Deletes the container
        /// </summary>
        /// <param name="containerName">The name of the container</param>
        /// <returns>True, if successful, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        Task<bool> DeleteContainerAsync(string containerName);

        /// <summary>
        /// Returns a reference to the container
        /// </summary>
        /// <param name="containerName">The container name</param>
        /// <returns>A reference to the container, if successful</returns>
        /// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        BlobContainerClient GetBlobContainerClient(string containerName);

        /// <summary>
        /// Returns a list of all of the containers in the current storage account
        /// </summary>
        /// <returns>A List of BlobContainerItems</returns>
        Task<IList<BlobContainerItem>> GetAllContainersAsync();
    }
}
