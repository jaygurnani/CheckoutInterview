namespace CheckoutInterview.Models.Requests
{
    using Newtonsoft.Json;

    public class GetResponse
    {
        [JsonProperty("paymentRecord")]
        public PaymentRecord PaymentRecord { get; set; }

        [JsonProperty("nextPaymentRecordId")]
        public int? NextPaymentRecordId { get; set; }
    }
}