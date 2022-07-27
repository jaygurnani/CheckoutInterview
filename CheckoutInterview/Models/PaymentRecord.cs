using System;
using Newtonsoft.Json;

namespace CheckoutInterview.Models
{
    public class PaymentRecord
    {
        [JsonProperty("paymentRecordId")]
        public int PaymentRecordId { get; set; }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("payment")]
        public PaymentModel Payment { get; set; }
    }
}
