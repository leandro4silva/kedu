using System.Text.Json.Serialization;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class RegisterBillingPaymentCommand : IRequest<RegisterBillingPaymentResponse>
    {
        [JsonIgnore]
        public int BillingId { get; set; }

        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [JsonPropertyName("dataPagamento")]
        public DateTime PaymentDate { get; set; }
    }

    public class RegisterBillingPaymentResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("valor")]
        public decimal Value { get; set; }

        [JsonPropertyName("valorPago")]
        public decimal PaidValue { get; set; }

        [JsonPropertyName("dataVencimento")]
        public DateTime DueDate { get; set; }

        [JsonPropertyName("dataPagamento")]
        public DateTime PaymentDate { get; set; }

        [JsonPropertyName("metodoPagamento")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("codigoPagamento")]
        public string PaymentCode { get; set; }

        [JsonPropertyName("idPlanoPagamento")]
        public int PaymentPlanId { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("dataModificacao")]
        public DateTime? ModifyDate { get; set; }

        public RegisterBillingPaymentResponse(int id, decimal value, decimal paidValue, DateTime dueDate, DateTime paymentDate, string paymentMethod, string status, string paymentCode, int paymentPlanId, DateTime createDate, DateTime? modifyDate)
        {
            Id = id;
            Value = value;
            PaidValue = paidValue;
            DueDate = dueDate;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            Status = status;
            PaymentCode = paymentCode;
            PaymentPlanId = paymentPlanId;
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
    }
}