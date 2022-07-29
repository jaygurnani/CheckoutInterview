namespace CheckoutInterview.Repositories
{
    using System;
    using CheckoutInterview.Models;

    public interface IPaymentRecordService
    {
        /// <summary>
        /// Gets a Payment record by merchantId and paymentId.
        /// </summary>
        /// <param name="merchantId">MerchantId</param>
        /// <param name="paymentRecordId">PaymentId.</param>
        /// <returns>The payment record itself and the next payment record Id if there is one.</returns>
        Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId);

        /// <summary>
        /// Inserts a payment into the database.
        /// </summary>
        /// <param name="merchantId">Merchant Id.</param>
        /// <param name="payment">Payment Id.</param>
        /// <returns>An ID of the newly created payment.</returns>
        int Insert(int merchantId, Payment payment);
    }
}