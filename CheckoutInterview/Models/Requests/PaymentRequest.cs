namespace CheckoutInterview.Models.Requests
{
    using Newtonsoft.Json;

    public class PaymentRequest
    {
        [JsonProperty("merchantId")]
        public int MerchantId { get; set; }

        [JsonProperty("payment")]
        public Payment Payment { get; set; }
    }
}