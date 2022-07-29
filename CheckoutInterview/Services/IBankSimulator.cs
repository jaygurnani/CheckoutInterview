namespace CheckoutInterview.Services
{
    public interface IBankSimulator
    {
        bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear);
    }
}