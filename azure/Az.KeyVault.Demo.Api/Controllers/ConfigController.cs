using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Az.KeyVault.Demo.Api.Controllers
{
    [ApiController]
    [Route("api/key-vault/config")]
    public class ConfigController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public object? State { get; }

        public ConfigController(IConfiguration configuration)
        {
            var releadToken = configuration.GetReloadToken();

           // releadToken.RegisterChangeCallback(Demo, State);

            _configuration = configuration;
        }

        //[HttpGet("reload")]
        //public ActionResult<string> Reload()
        //{
        //    var configRoot = _configuration as IConfigurationRoot;

        //    if (configRoot != null)
        //    {
        //        configRoot.Reload();
        //        return Ok("Application ConfigurationSetting Refreshed...!");
        //    }
        //    else
        //    {
        //        return Ok("Application ConfigurationSetting Not Refreshed...!");
        //    }
        //}

        //private void Demo(object? obj)
        //{
        //    throw new NotImplementedException();
        //}

        [HttpGet]
        public string Get()
        {
            return _configuration.GetValue(typeof(string), "DemoKey").ToString();
        }
    }
}