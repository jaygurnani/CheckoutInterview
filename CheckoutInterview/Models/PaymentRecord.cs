namespace CheckoutInterview.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class PaymentRecord
    {
        [JsonProperty("paymentRecordId")]
        public int PaymentRecordId { get; set; }

        [JsonProperty("merchantId")]
        public int MerchantId { get; set; }

        [JsonProperty("success")]
        public bool IsSuccess { get; set; }

        [JsonProperty("payment")]
        public Payment Payment { get; set; }
    }
}
