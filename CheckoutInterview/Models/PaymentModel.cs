using Newtonsoft.Json;

namespace CheckoutInterview.Models
{
    public class PaymentModel
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
        public Currency Currency { get; set; }

        [JsonProperty("cardIssuer")]
        public CardBrand CardIssuer { get; set; }

        [JsonProperty("cvv")]
        public string CVV { get; set; }
    }
}

