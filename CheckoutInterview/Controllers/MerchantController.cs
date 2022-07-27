namespace CheckoutInterview.Controllers
{
    using System;
    using CheckoutInterview.Models.Requests;
    using CheckoutInterview.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class MerchantController : ControllerBase
    {

        private readonly ILogger<MerchantController> _logger;
        private readonly IPaymentRecordService _paymentRecordService;

        public MerchantController(ILogger<MerchantController> logger, IPaymentRecordService paymentRecordService)
        {
            _logger = logger;
            _paymentRecordService = paymentRecordService;
        }

        [HttpGet]
        public IActionResult Get([FromQuery(Name = "paymentRecordId")] int paymentRecordId, [FromQuery(Name = "merchantId")] int merchantId)
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
