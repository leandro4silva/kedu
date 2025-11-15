using Kedu.Domain.Enums;

namespace Kedu.Application.Extensions
{
    public static class EnumExtensions
    {
        public static string ToPortugueseString(this StatusBilling status)
        {
            return status switch
            {
                StatusBilling.Issued => "EMITIDA",
                StatusBilling.Paid => "PAGA",
                StatusBilling.Cancelled => "CANCELADA",
                _ => status.ToString()
            };
        }

        public static string ToPortugueseString(this PaymentMethod paymentMethod)
        {
            return paymentMethod switch
            {
                PaymentMethod.Boleto => "BOLETO",
                PaymentMethod.Pix => "PIX",
                _ => paymentMethod.ToString()
            };
        }
    }
}