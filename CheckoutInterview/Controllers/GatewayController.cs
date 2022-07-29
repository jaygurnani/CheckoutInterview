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
        [Route("{merchantId:int}/{paymentRecordId:int}")]
        public IActionResult Get(int merchantId, int paymentRecordId)
        {
            try
            {
                var getPaymentRecord = _paymentRecordService.GetPaymentRecord(merchantId, paymentRecordId);
                var response = new GetResponse
                {
                    PaymentRecord = getPaymentRecord.Item1,
                    NextItem = getPaymentRecord.Item2,
                };

                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("{merchantId:int}")]
        public IActionResult Post(int merchantId, [FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var recordPayment = _paymentRecordService.Insert(merchantId, paymentRequest.Payment);
                return Ok(recordPayment);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
