using duholiya.communication.service.data.Contracts;
using duholiya.communication.service.Services;
using Microsoft.AspNetCore.Mvc;

namespace duholiya.communication.service.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("duholiya/communication/sms/otp")]
    public class OtpController : ControllerBase
    {
        private readonly ILogger<OtpController> _logger;

        private readonly IOtpRepository _otpRepository;

        private readonly QueueingService _queueingService;

        public OtpController(QueueingService queueingService, IOtpRepository otpRepository, ILogger<OtpController> logger)
        {
            _queueingService = queueingService;
            _otpRepository = otpRepository;
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <returns></returns>
        [HttpPost("{mobileNumber}/request-new")]
        public async Task<ActionResult<string>> RequestOtpAsync(string mobileNumber)
        {
            // Verify if already regsitered
            var userExists = await UserExists(mobileNumber);

            if (!userExists)
            {
                return Unauthorized("Unregistered mobile number");
            }

            // Add to service bus
            await _queueingService.SendAsync(mobileNumber);

            return Accepted();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mobileNumber"></param>
        /// <param name="otp"></param>
        /// <returns></returns>
        [HttpGet("{mobileNumber}/verify")]
        public Task<IActionResult> VerifyOtpAsync(string mobileNumber, string otp)
        {
            // Verify OTP
            // Return status
            throw new NotImplementedException();
        }

        private async Task<bool> UserExists(string mobileNumber)
        {
            return await Task.FromResult(true);
        }
    }
}