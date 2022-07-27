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
                        CreditCardNumber = CreditCardFactory.RandomCardNumber(MapToCardIssuer(GetRandomEnum<CardBrand>())),
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

        public static CardIssuer MapToCardIssuer(CardBrand input)
        {
            CardIssuer internalCard;
            switch (input)
            {
                case CardBrand.VISA:
                    internalCard = CardIssuer.Visa;
                    break;
                case CardBrand.AMEX:
                    internalCard = CardIssuer.AmericanExpress;
                    break;
                case CardBrand.MASTERCARD:
                    internalCard = CardIssuer.MasterCard;
                    break;
                default:
                    internalCard = CardIssuer.Visa;
                    break;
            }

            return internalCard;
        }
    }
}