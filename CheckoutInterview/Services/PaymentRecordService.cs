namespace CheckoutInterview.Repositories
{
    using System;
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

        public Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId)
        {
            var getPaymentRecord = _paymentRecordRepository.GetPaymentRecord(merchantId, paymentRecordId);
            return Tuple.Create(getPaymentRecord.Item1, getPaymentRecord.Item2);
        }

        public int Insert(int merchantId, Payment payment)
        {
            var validator = new CreditCardValidator.CreditCardDetector(payment.CreditCardNumber);
            if (!validator.IsValid())
            {
                throw new ApplicationException("Credit card number is invalid");
            }

            int paymentRecordId = _paymentRecordRepository.Insert(merchantId, payment, false);
            return paymentRecordId;
        }
    }
}