namespace CheckoutInterview.MockDBSeeder
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using CheckoutInterview.Models;
    using CreditCardValidator;
    using Newtonsoft.Json;

    public static class DBHelper
    {
        // We use this to seed the database
        public static void SeedDb(int size)
        {
            List<PaymentRecord> data = new List<PaymentRecord>();

            for (int i = 1; i <= size; i++)
            {
                PaymentRecord item = new PaymentRecord
                {
                    PaymentRecordId = i,
                    MerchantId = new Random().Next(1, 5),
                    IsSuccess = i % 2 == 0,
                    Payment = new Payment
                    {
                        CreditCardNumber = CreditCardFactory.RandomCardNumber(GetRandomCardIssuer()),
                        ExpiryMonth = new Random().Next(1, 12).ToString(),
                        ExpiryYear = new Random().Next(10, 25).ToString(),
                        Amount = new Random().Next(1, 1000),
                        Currency = GetRandomEnum<Currency>(),
                        CVV = new Random().Next(111, 999).ToString(),
                    },
                };

                data.Add(item);
            }

            using (StreamWriter f = new StreamWriter("./MockDB.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(f, data);
            }
        }

        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random random = new Random();
            T randomEnum = (T)values.GetValue(random.Next(values.Length));
            return randomEnum;
        }

        // We pick from the below Enums because the Solo card scheme has been deprecated and we should not use it
        public static CardIssuer GetRandomCardIssuer()
        {
            Array values = new List<CardIssuer>()
            {
                CardIssuer.Visa,
                CardIssuer.AmericanExpress,
                CardIssuer.MasterCard,
            }.ToArray();

            Random random = new Random();
            return (CardIssuer)values.GetValue(random.Next(values.Length));
        }
    }
}