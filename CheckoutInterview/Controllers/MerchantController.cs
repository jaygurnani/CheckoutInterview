namespace CheckoutInterview.Controllers
{
    using System;
    using CheckoutInterview.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class MerchantController : ControllerBase
    {

        private readonly ILogger<MerchantController> _logger;

        public MerchantController(ILogger<MerchantController> logger, IMockDBRepository mockDBRepository)
        {
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            var rng = new Random();
            return rng.ToString();
        }
    }
}
