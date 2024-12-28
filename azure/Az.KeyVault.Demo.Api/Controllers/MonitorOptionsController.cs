using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Az.KeyVault.Demo.Api.Controllers
{
    [Route("key-vault-config-options/monitor")]
    [ApiController]
    public class MonitorOptionsController : ControllerBase
    {
        private PositionOptions _options;

        public MonitorOptionsController(IOptionsMonitor<PositionOptions> options)
        {
            options.OnChange(UpdateConfiguration);

            _options = options.CurrentValue;
        }


        private void UpdateConfiguration(PositionOptions config)
        {
            _options = config;
        }

        [HttpGet]
        public string Get()
        {
            //return _configuration.GetValue(typeof(string), "DemoKey").ToString();

            return _options.Name;
        }
    }
}
