using System.Text.Json.Serialization;

namespace Kedu.Application.Dtos
{
    public class PaymentPlanDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; set; }

        [JsonPropertyName("idResponsavel")]
        public int FinancialManagerId { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("dataModificacao")]
        public DateTime? ModifyDate { get; set; }

        [JsonPropertyName("cobrancas")]
        public List<BillingDto> Billings { get; set; } = new();

        public PaymentPlanDto(int id, decimal totalValue, int financialManagerId, DateTime createDate, DateTime? modifyDate)
        {
            Id = id;
            TotalValue = totalValue;
            FinancialManagerId = financialManagerId;
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
    }
}
