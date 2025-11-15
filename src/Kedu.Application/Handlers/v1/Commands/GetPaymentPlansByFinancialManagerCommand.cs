
using System.Text.Json.Serialization;
using Kedu.Application.Dtos;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class GetPaymentPlansByFinancialManagerCommand : IRequest<GetPaymentPlansByFinancialManagerResponse>
    {
        public int FinancialManagerId { get; set; }
    }

    public class GetPaymentPlansByFinancialManagerResponse
    {
        [JsonPropertyName("idResponsavel")]
        public int IdFinancialManager { get; set; }

        [JsonPropertyName("nomeResponsavel")]
        public string NameFinancialManager { get; set; }

        [JsonPropertyName("planosDePagamento")]
        public List<PaymentPlanDto> PaymentPlans { get; set; }

        public GetPaymentPlansByFinancialManagerResponse(int idFinancialManager, string nameFinancialManager, List<PaymentPlanDto> paymentPlans)
        {
            PaymentPlans = paymentPlans;
            NameFinancialManager = nameFinancialManager;
            IdFinancialManager = idFinancialManager;
        }
    }
}
