namespace CheckoutInterview.Services
{
    using System;

    public class BankSimulator : IBankSimulator
    {
        public bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear)
        {
            return new Random().Next() % 2 == 0;
        }
    }
}