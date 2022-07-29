namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;

    public interface IPaymentRecordRepository
    {
        // Get a payment record
        Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId);

        // Return the Payment Record Id
        int Insert(int merchantId, Payment payment, bool isSuccess);
    }
}