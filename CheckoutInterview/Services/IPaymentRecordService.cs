namespace CheckoutInterview.Repositories
{
    using CheckoutInterview.Models;

    public interface IPaymentRecordService
    {
        PaymentRecord GetPaymentRecord(int paymentRecordId, int merchantId);

        int Insert(Payment payment, int merchantId);
    }
}