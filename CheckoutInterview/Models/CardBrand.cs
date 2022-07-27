namespace CheckoutInterview.Models
{
    using System.Runtime.Serialization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    [JsonConverter(typeof(StringEnumConverter))]
    public enum CardBrand
    {
        VISA,
        AMEX,
        MASTERCARD,
    }
}