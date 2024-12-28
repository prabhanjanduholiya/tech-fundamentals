using Az.Samples.BlobStorage.Lib.Contracts;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;

namespace Az.Samples.BlobStorage.Demo.Controllers
{
    [ApiController]
    [Route("blob-storage/api/blob-containers")]
    public class BlobContainerController : ControllerBase
    {
        private readonly ILogger<BlobContainerController> _logger;

        private readonly IBlobContainerManager _blobContainerManager;

        public BlobContainerController(IBlobContainerManager blobContainerManager, ILogger<BlobContainerController> logger)
        {
            _logger = logger;
            _blobContainerManager = blobContainerManager;
        }

        /// <summary>
        /// Create new blob container
        /// </summary>
        /// <param name="containerName"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BlobContainerClient> CreateBlobContainerAsync(string containerName)
        {
            return await _blobContainerManager.CreateBlobContainerAsync(containerName);
        }

        /// <summary>
        /// Deletes the container
        /// </summary>
        /// <param name="containerName">The name of the container</param>
        /// <returns>True, if successful, otherwise, false</returns>
        /// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteContainerAsync(string containerName)
        {
            try
            {
                return await _blobContainerManager.DeleteContainerAsync(containerName);
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///// <summary>
        ///// Returns a reference to the container
        ///// </summary>
        ///// <param name="containerName">The container name</param>
        ///// <returns>A reference to the container, if successful</returns>
        ///// <exception cref="ArgumentNullException">Throws if the containerName is null or empty</exception>
        //[HttpGet("{containerName}")]
        //public BlobContainerClient GetBlobContainerClient(string containerName)
        //{
        //    return _blobContainerManager.GetBlobContainerClient(containerName);
        //}

        /// <summary>
        /// Returns a list of all of the containers in the current storage account
        /// </summary>
        /// <returns>A List of BlobContainerItems</returns>
        [HttpGet]
        public async Task<IList<BlobContainerItem>> GetAllContainersAsync()
        {
            return await _blobContainerManager.GetAllContainersAsync();
        }
    }
}