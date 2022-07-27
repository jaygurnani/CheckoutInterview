namespace CheckoutInterview.Repositories
{
    using CheckoutInterview.Models;
    using Microsoft.Extensions.Logging;

    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly ILogger<PaymentRecordService> _logger;

        public PaymentRecordService(IPaymentRecordRepository paymentRecordRepository, ILogger<PaymentRecordService> logger)
        {
            _paymentRecordRepository = paymentRecordRepository;
            _logger = logger;
        }

        public PaymentRecord GetPaymentRecord(int paymentRecordId, int merchantId)
        {
            return _paymentRecordRepository.GetPaymentRecord(paymentRecordId, merchantId);
        }

        public int Insert(Payment payment, int merchantId)
        {
            int paymentRecordId = _paymentRecordRepository.Insert(payment, merchantId, false);
            return paymentRecordId;
        }
    }
}