namespace DependencyInjectionDemo.Models;

using System.Runtime.Serialization;
using System.Text.Json.Serialization;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum PaymentProvider
{
    [EnumMember(Value = "stripe")]
    Stripe,

    [EnumMember(Value = "paypal")]
    PayPal,

    [EnumMember(Value = "crypto")]
    Crypto
}