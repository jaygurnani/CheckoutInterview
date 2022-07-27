namespace CheckoutInterview.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CheckoutInterview.Models;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    public class MockDBRepository : IMockDBRepository
    {
        private readonly List<PaymentRecord> _mockDB;
        private readonly ILogger<MockDBRepository> _logger;

        public MockDBRepository(ILogger<MockDBRepository> logger)
        {
            _mockDB = JsonConvert.DeserializeObject<List<PaymentRecord>>(File.ReadAllText(@"MockDB.json"));
            _logger = logger;
        }

        public PaymentRecord GetByPaymend(int paymentRecordId, int merchantId)
        {
            return _mockDB.Where(x => x.PaymentRecordId == paymentRecordId && x.MerchantId == merchantId).SingleOrDefault();
        }

        public void Insert(PaymentRecord paymentRecord)
        {
            throw new NotImplementedException();
        }
    }
}