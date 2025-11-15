using System.Text.Json.Serialization;
using Kedu.Application.Dtos;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class GetPaymentPlanByIdCommand : IRequest<GetPaymentPlanByIdResponse>
    {
        public int Id { get; }

        [JsonConstructor]
        public GetPaymentPlanByIdCommand(int id)
        {
            Id = id;
        }
    }

    public class GetPaymentPlanByIdResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; set; }

        [JsonPropertyName("responsavel")]
        public FinancialManagerDto FinancialManager { get; set; }

        [JsonPropertyName("centroDeCusto")]
        public CostCenterDto CostCenter { get; set; }

        [JsonPropertyName("cobrancas")]
        public List<BillingDto> Billings { get; set; } = new();

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("dataModificacao")]
        public DateTime? ModifyDate { get; set; }

        public GetPaymentPlanByIdResponse(int id, decimal totalValue, DateTime createDate, DateTime? modifyDate)
        {
            Id = id;
            TotalValue = totalValue;
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
    } 
}
