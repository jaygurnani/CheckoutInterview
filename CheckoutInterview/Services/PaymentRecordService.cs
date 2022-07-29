namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;
    using CheckoutInterview.Services;
    using Microsoft.Extensions.Logging;

    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IBankSimulator _bankSimulator;
        private readonly ILogger<PaymentRecordService> _logger;

        public PaymentRecordService(IPaymentRecordRepository paymentRecordRepository, IBankSimulator bankSimulator, ILogger<PaymentRecordService> logger)
        {
            _paymentRecordRepository = paymentRecordRepository;
            _bankSimulator = bankSimulator;
            _logger = logger;
        }

        public Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId)
        {
            var getPaymentRecord = _paymentRecordRepository.GetPaymentRecord(merchantId, paymentRecordId);
            var paymentRecord = getPaymentRecord.Item1;
            var cardNumber = paymentRecord.Payment.CreditCardNumber;
            var maskedCard = cardNumber.Substring(0, cardNumber.Length - 3);


            paymentRecord.Payment.CreditCardNumber = cardNumber.Replace(maskedCard, "****-****-****-");
            return Tuple.Create(paymentRecord, getPaymentRecord.Item2);
        }

        public int Insert(int merchantId, Payment payment)
        {
            var validator = new CreditCardValidator.CreditCardDetector(payment.CreditCardNumber);
            if (!validator.IsValid())
            {
                throw new ApplicationException("Credit card number is invalid");
            }

            bool result = _bankSimulator.MakePayment(payment.CreditCardNumber, payment.CVV, payment.ExpiryMonth, payment.ExpiryYear);

            int paymentRecordId = _paymentRecordRepository.Insert(merchantId, payment, result);
            return paymentRecordId;
        }
    }
}