using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Az.KeyVault.Demo.Api.Controllers
{
    [Route("key-vault-config-options/snapshot")]
    [ApiController]
    public class OptionsSnapshotController : ControllerBase
    {
        private readonly PositionOptions _options;

        public OptionsSnapshotController(IOptionsSnapshot<PositionOptions> options)
        {
            _options = options.Value;
        }

        [HttpGet]
        public string Get()
        {
            //return _configuration.GetValue(typeof(string), "DemoKey").ToString();

            return _options.Name;
        }
    }
}
