namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;

    public interface IPaymentRecordService
    {
        Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId);

        int Insert(int merchantId, Payment payment);
    }
}