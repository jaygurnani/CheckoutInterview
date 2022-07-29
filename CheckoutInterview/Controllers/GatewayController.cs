namespace CheckoutInterview.Controllers
{
    using System;
    using CheckoutInterview.Models.Requests;
    using CheckoutInterview.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger<GatewayController> _logger;
        private readonly IPaymentRecordService _paymentRecordService;

        public GatewayController(ILogger<GatewayController> logger, IPaymentRecordService paymentRecordService)
        {
            _logger = logger;
            _paymentRecordService = paymentRecordService;
        }

        [HttpGet]
        [Route("{paymentRecordId:int}/{merchantId:int}")]
        public IActionResult Get(int paymentRecordId, int merchantId)
        {
            var recordPayment = _paymentRecordService.GetPaymentRecord(paymentRecordId, merchantId);
            return Ok(recordPayment);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PaymentRequest paymentRequest)
        {
            var recordPayment = _paymentRecordService.Insert(paymentRequest.Payment, paymentRequest.MerchantId);
            return Ok(recordPayment);
        }
    }
}
