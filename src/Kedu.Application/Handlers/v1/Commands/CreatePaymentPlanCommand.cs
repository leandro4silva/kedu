using System.Text.Json.Serialization;
using MediatR;
using Kedu.Domain.Enums;
using Kedu.Application.Dtos;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class CreatePaymentPlanCommand : IRequest<CreatePaymentPlanResponse>
    {
        [JsonPropertyName("responsavelId")]
        public int FinancialManagerId { get; set; }

        [JsonPropertyName("centroDeCusto")]
        public int CostCenterId { get; set; }

        [JsonPropertyName("cobrancas")]
        public List<BillingItem> Billings { get; set; } = new();
    }

    public class BillingItem
    {
        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [JsonPropertyName("dataVencimento")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("metodoPagamento")]
        public PaymentMethod PaymentMethod { get; set; }
    }

    public class CreatePaymentPlanResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("responsavelId")]
        public int FinancialManagerId { get; set; }

        [JsonPropertyName("centroDeCusto")]
        public int CostCenterId { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; set; }

        [JsonPropertyName("cobrancas")]
        public List<BillingDto> Billings { get; set; } = new();

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        public CreatePaymentPlanResponse(int id, int financialManagerId, int costCenterId, decimal totalValue, DateTime createDate)
        {
            Id = id;
            FinancialManagerId = financialManagerId;
            CostCenterId = costCenterId;
            TotalValue = totalValue;
            CreateDate = createDate;
        }
    }
}