namespace CheckoutInterview.Repositories
{
    using CheckoutInterview.Models;

    public interface IMockDBRepository
    {
        PaymentRecord GetByPaymend(int paymentRecordId, int merchantId);

        void Insert(PaymentRecord paymentRecord);
    }
}