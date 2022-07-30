namespace CheckoutInterview.Services
{
    using System;

    public class BankService : IBankService
    {
        public bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear)
        {
            // Pick `true` or `false` at random if the reques was a sucess
            return new Random().Next() % 2 == 0;
        }
    }
}