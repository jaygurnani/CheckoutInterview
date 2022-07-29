namespace CheckoutInterview.Services
{
    public interface IBankSimulator
    {
        /// <summary>
        /// Makes a payment to the bank.
        /// </summary>
        /// <param name="creditCardNumber">Credit Card number.</param>
        /// <param name="cvv">CVV.</param>
        /// <param name="expiryMonth">Expiry Month.</param>
        /// <param name="expiryYear">Expiry Year.</param>
        /// <returns>If the payment was successful or not.</returns>
        bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear);
    }
}