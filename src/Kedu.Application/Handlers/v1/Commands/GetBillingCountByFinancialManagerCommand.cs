using System.Text.Json.Serialization;
using Kedu.Application.Dtos;
using Kedu.Domain.Enums;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class GetBillingCountByFinancialManagerCommand : IRequest<GetBillingCountByFinancialManagerResponse>
    {
        public int FinancialManagerId { get; }

        [JsonPropertyName("status")]
        public StatusBilling? Status { get; }

        [JsonPropertyName("metodoPagamento")]
        public PaymentMethod? PaymentMethod { get; }

        [JsonConstructor]
        public GetBillingCountByFinancialManagerCommand(
            int financialManagerId,
            StatusBilling? status = null,
            PaymentMethod? paymentMethod = null)
        {
            FinancialManagerId = financialManagerId;
            Status = status;
            PaymentMethod = paymentMethod;
        }
    }

    public class GetBillingCountByFinancialManagerResponse
    {
        [JsonPropertyName("idResponsavel")]
        public int FinancialManagerId { get; set; }

        [JsonPropertyName("nomeResponsavel")]
        public string FinancialManagerName { get; set; }

        [JsonPropertyName("quantidade")]
        public int Count { get; set; }

        [JsonPropertyName("filtros")]
        public BillingFiltersDto Filters { get; set; }

        public GetBillingCountByFinancialManagerResponse(int financialManagerId, string financialManagerName, int count, BillingFiltersDto filters)
        {
            FinancialManagerId = financialManagerId;
            FinancialManagerName = financialManagerName;
            Count = count;
            Filters = filters;
        }
    }
}
