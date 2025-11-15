using MediatR;
using System.Text.Json.Serialization;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class GetPaymentPlanTotalCommand : IRequest<GetPaymentPlanTotalResponse>
    {
        public int Id { get; }

        [JsonConstructor]
        public GetPaymentPlanTotalCommand(int id)
        {
            Id = id;
        }
    }

    public class GetPaymentPlanTotalResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("valorTotal")]
        public decimal TotalValue { get; set; }

        [JsonPropertyName("quantidadeCobrancas")]
        public int BillingCount { get; set; }

        public GetPaymentPlanTotalResponse(int id, decimal totalValue, int billingCount)
        {
            Id = id;
            TotalValue = totalValue;
            BillingCount = billingCount;
        }
    }
}