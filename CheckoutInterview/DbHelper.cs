using System;
using System.Collections.Generic;
using System.IO;
using CheckoutInterview.Models;
using CreditCardValidator;
using Newtonsoft.Json;

namespace CheckoutInterview.MockDBSeeder
{
    public static class DbHelper
    {
        public static void SeedDb(int size)
        {
            List<PaymentRecord> _data = new List<PaymentRecord>();

            for(int i = 1; i <= size; i++)
            {
                PaymentRecord item = new PaymentRecord
                {
                    PaymentRecordId = i,
                    Success = (i % 2 == 0),
                    Payment = new PaymentModel
                    {
                        CreditCardNumber = CreditCardFactory.RandomCardNumber(GetRandomEnum<CardIssuer>()),
                        ExpiryMonth = new Random().Next(1, 12).ToString(),
                        ExpiryYear = new Random().Next(10, 25).ToString(),
                        Amount = new Random().Next(1, 1000),
                        Currency = GetRandomEnum<Currency>()
                    }
                };

                _data.Add(item);
            }

            using (StreamWriter f = new StreamWriter("./MockDB.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(f, _data);
            }
        }


        public static T GetRandomEnum<T>()
        {
            Array values = Enum.GetValues(typeof(T));
            Random random = new Random();
            T randomEnum = (T)values.GetValue(random.Next(values.Length));
            return randomEnum;
        }
                
    }
}

