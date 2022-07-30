namespace CheckoutInterview.Controllers
{
    using System;
    using CheckoutInterview.Models.Requests;
    using CheckoutInterview.Repositories;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("api/v1/[controller]")]
    public class GatewayController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IPaymentRecordService _paymentRecordService;

        public GatewayController(ILogger<GatewayController> logger, IPaymentRecordService paymentRecordService)
        {
            _logger = logger;
            _paymentRecordService = paymentRecordService;
        }

        /// <summary>
        /// Gets a previously created payment record.
        /// </summary>
        /// <param name="merchantId">The merchant's Id.</param>
        /// <param name="paymentRecordId">The payment record Id.</param>
        /// <returns>The payment record.</returns>
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
                    NextPaymentRecordId = getPaymentRecord.Item2,
                };

                return Ok(response);
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Creates a new payment record and returns the new payment record Id.
        /// </summary>
        /// <param name="merchantId">The merchants Id.</param>
        /// <param name="paymentRequest">The payment request.</param>
        /// <returns>The newly created payment record Id.</returns>
        [HttpPost]
        [Route("{merchantId:int}")]
        public IActionResult Post(int merchantId, [FromBody] PaymentRequest paymentRequest)
        {
            try
            {
                var paymentRecordId = _paymentRecordService.Insert(merchantId, paymentRequest.Payment);
                return Ok(new { paymentRecordId });
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
