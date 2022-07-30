namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;
    using CheckoutInterview.Services;
    using Microsoft.Extensions.Logging;

    public class PaymentRecordService : IPaymentRecordService
    {
        private readonly IPaymentRecordRepository _paymentRecordRepository;
        private readonly IBankService _bankService;
        private readonly ILogger _logger;

        public PaymentRecordService(IPaymentRecordRepository paymentRecordRepository, IBankService bankService, ILogger<PaymentRecordService> logger)
        {
            _paymentRecordRepository = paymentRecordRepository;
            _bankService = bankService;
            _logger = logger;
        }

        public Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId)
        {
            var getPaymentRecord = _paymentRecordRepository.GetPaymentRecord(merchantId, paymentRecordId);
            var paymentRecord = getPaymentRecord.Item1;
            var cardNumber = paymentRecord.Payment.CreditCardNumber;

            // This logic is for masking the credit card on return
            var maskedCard = cardNumber.Substring(0, cardNumber.Length - 3);

            // We assume that all credit cards have 12+ digits
            paymentRecord.Payment.CreditCardNumber = cardNumber.Replace(maskedCard, "****-****-****-");
            return Tuple.Create(paymentRecord, getPaymentRecord.Item2);
        }

        public int Insert(int merchantId, Payment payment)
        {
            var validator = new CreditCardValidator.CreditCardDetector(payment.CreditCardNumber);

            // If the credit card number is not valid, throw an exception
            if (!validator.IsValid())
            {
                throw new ApplicationException("Credit card number is invalid");
            }

            // Make the call to the banking service and record its response in the DB
            bool result = _bankService.MakePayment(payment.CreditCardNumber, payment.CVV, payment.ExpiryMonth, payment.ExpiryYear);

            int paymentRecordId = _paymentRecordRepository.Insert(merchantId, payment, result);
            return paymentRecordId;
        }
    }
}