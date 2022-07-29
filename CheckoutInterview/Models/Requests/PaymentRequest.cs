namespace CheckoutInterview.Models.Requests
{
    using Newtonsoft.Json;

    public class PaymentRequest
    {
        [JsonProperty("payment")]
        public Payment Payment { get; set; }
    }
}