using System.Text.Json.Serialization;


namespace Kedu.Application.Dtos
{
    public class BillingFiltersDto
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("metodoPagamento")]
        public string PaymentMethod { get; set; }

        public BillingFiltersDto(string status, string paymentMethod)
        {
            Status = status;
            PaymentMethod = paymentMethod;
        }
    }
}
