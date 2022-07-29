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

        public Tuple<PaymentRecord, int?> GetPaymentRecord(int merchantId, int paymentRecordId)
        {
            // Empty cursor
            int? nextItem;

            // Get the data by merchantId
            var merchantRecords = _mockDB.Where(x => x.MerchantId == merchantId).ToList();

            var merchantTotalCount = merchantRecords.Count();
            var currentRecord = merchantRecords.Where(x => x.PaymentRecordId == paymentRecordId).SingleOrDefault();

            // If there is no record, return an error
            if (currentRecord == null)
            {
                throw new ArgumentException($"PaymentRecordId: {paymentRecordId} for merchant: {merchantId} does not exist");
            }

            var currentRecordIndex = merchantRecords.IndexOf(currentRecord);
            int nextRecordIndex = currentRecordIndex + 1;

            // This logic handles the cursor
            if (merchantTotalCount <= nextRecordIndex)
            {
                nextItem = null;
            } else
            {
                var nextRecord = merchantRecords[nextRecordIndex];
                nextItem = nextRecord.PaymentRecordId;
            }

            return Tuple.Create(currentRecord, nextItem);
        }

        public int Insert(int merchantId, Payment payment, bool isSuccess)
        {
            // For the newly created record, get the next count in the DB
            var newCount = _mockDB.Count + 1;
            var item = new PaymentRecord
            {
                PaymentRecordId = newCount,
                MerchantId = merchantId,
                IsSuccess = isSuccess,
                Payment = payment,
            };

            _mockDB.Add(item);

            // TODO We should find a way to incrmentally add to the DB rather than
            // flushing all records to disk
            using (StreamWriter f = new StreamWriter(_mockDBFile))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(f, _mockDB);
            }

            return newCount;
        }
    }
}