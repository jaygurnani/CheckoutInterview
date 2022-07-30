namespace CheckoutInterview.Services
{
    using System;
    using Microsoft.Extensions.Logging;

    public class BankService : IBankService
    {
        private readonly ILogger _logger;

        public BankService(ILogger<BankService> logger)
        {
            _logger = logger;
        }

        public bool MakePayment(string creditCardNumber, string cvv, string expiryMonth, string expiryYear)
        {
            // Pick `true` or `false` at random if the reques was a sucess
            return new Random().Next() % 2 == 0;
        }
    }
}