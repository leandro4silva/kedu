using System.Text.Json.Serialization;
using Kedu.Application.Dtos;
using Kedu.Domain.Enums;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class GetBillingsByFinancialManagerCommand : IRequest<GetBillingsByFinancialManagerResponse>
    {
        public int FinancialManagerId { get; }

        [JsonPropertyName("status")]
        public StatusBilling? Status { get; }

        [JsonPropertyName("metodoPagamento")]
        public PaymentMethod? PaymentMethod { get; }

        [JsonPropertyName("dataInicio")]
        public DateTime? StartDate { get; }

        [JsonPropertyName("dataFim")]
        public DateTime? EndDate { get; }

        [JsonConstructor]
        public GetBillingsByFinancialManagerCommand(
            int financialManagerId,
            StatusBilling? status = null,
            PaymentMethod? paymentMethod = null,
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            FinancialManagerId = financialManagerId;
            Status = status;
            PaymentMethod = paymentMethod;
            StartDate = startDate;
            EndDate = endDate;
        }

    }

    public class GetBillingsByFinancialManagerResponse
    {
        [JsonPropertyName("idResponsavel")]
        public int FinancialManagerId { get; set; }

        [JsonPropertyName("nomeResponsavel")]
        public string FinancialManagerName { get; set; }

        [JsonPropertyName("cobrancas")]
        public List<BillingDto> Billings { get; set; }

        [JsonPropertyName("total")]
        public int TotalCount { get; set; }

        public GetBillingsByFinancialManagerResponse(int financialManagerId, string financialManagerName, List<BillingDto> billings, int totalCount)
        {
            FinancialManagerId = financialManagerId;
            FinancialManagerName = financialManagerName;
            Billings = billings;
            TotalCount = totalCount;
        }
    }
}
