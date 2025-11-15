using System.Text.Json.Serialization;
using Kedu.Application.Converters;

namespace Kedu.Domain.Enums
{
    [JsonConverter(typeof(CaseInsensitiveEnumConverter<PaymentMethod>))]
    public enum PaymentMethod
    {
        Boleto,
        Pix
    }
}
