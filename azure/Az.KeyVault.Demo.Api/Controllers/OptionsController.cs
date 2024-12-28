using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Az.KeyVault.Demo.Api.Controllers
{
    [Route("api/key-vault/config/options")]
    [ApiController]
    public class OptionsController : ControllerBase
    {
        private readonly PositionOptions _options;

        public OptionsController(IOptions<PositionOptions> options)
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
