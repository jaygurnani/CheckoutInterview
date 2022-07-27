namespace CheckoutInterview.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CheckoutInterview.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class MockDBRepository : IPaymentRecordRepository
    {
        private readonly List<PaymentRecord> _mockDB;
        private readonly ILogger<MockDBRepository> _logger;
        private readonly string _mockDBFile = "./MockDB.json";

        public MockDBRepository(ILogger<MockDBRepository> logger)
        {
            _mockDB = JsonConvert.DeserializeObject<List<PaymentRecord>>(File.ReadAllText(@"MockDB.json"));
            _logger = logger;
        }

        public PaymentRecord GetPaymentRecord(int paymentRecordId, int merchantId)
        {
            return _mockDB.Where(x => x.PaymentRecordId == paymentRecordId && x.MerchantId == merchantId).SingleOrDefault();
        }

        public int Insert(Payment payment, int merchantId, bool isSuccess)
        {
            var newCount = _mockDB.Count + 1;
            var item = new PaymentRecord
            {
                PaymentRecordId = newCount,
                MerchantId = merchantId,
                IsSuccess = isSuccess,
                Payment = payment,
            };

            _mockDB.Add(item);

            using (StreamWriter f = new StreamWriter(_mockDBFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(f, _mockDB);
            }

            return newCount;
        }
    }
}