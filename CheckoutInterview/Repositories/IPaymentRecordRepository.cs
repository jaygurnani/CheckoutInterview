namespace CheckoutInterview.Repositories
{
    using CheckoutInterview.Models;

    public interface IPaymentRecordRepository
    {
        PaymentRecord GetPaymentRecord(int paymentRecordId, int merchantId);

        // Return the Payment Record Id
        int Insert(Payment payment, int merchantId, bool isSuccess);
    }
}