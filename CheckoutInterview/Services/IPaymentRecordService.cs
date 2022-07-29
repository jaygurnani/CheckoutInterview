namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;

    public interface IPaymentRecordService
    {
        // Returns a payment record
        Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId);

        // Inserts a payment record
        int Insert(int merchantId, Payment payment);
    }
}