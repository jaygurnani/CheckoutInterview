namespace CheckoutInterview.Models
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum Currency
    {
        USD,
        GBP,
        EUR,
        AUD,
    }
}