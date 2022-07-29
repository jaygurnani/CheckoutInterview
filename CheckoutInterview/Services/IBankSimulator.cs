namespace CheckoutInterview.Services
{
    public interface IBankSimulator
    {
        // Simulator to make a payment. Returns if the payment was successful or not
        bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear);
    }
}