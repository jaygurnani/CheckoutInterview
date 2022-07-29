namespace CheckoutInterview.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public class Payment
    {
        [JsonProperty("creditCardNumber")]
        public string CreditCardNumber { get; set; }

        [JsonProperty("expiryMonth")]
        public string ExpiryMonth { get; set; }

        [JsonProperty("expiryYear")]
        public string ExpiryYear { get; set; }

        [JsonProperty("amount")]
        public decimal Amount { get; set; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Currency Currency { get; set; }

        [JsonProperty("cvv")]
        public string CVV { get; set; }
    }
}