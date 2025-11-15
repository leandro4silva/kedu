using System.Text.Json.Serialization;
using Kedu.Application.Converters;

namespace Kedu.Domain.Enums
{
    [JsonConverter(typeof(CaseInsensitiveEnumConverter<StatusBilling>))]
    public enum StatusBilling
    {
        Issued,
        Paid,
        Cancelled
    }
}
