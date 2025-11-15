using Kedu.Application.Dtos;
using Kedu.Application.Exceptions;
using Kedu.Application.Extensions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetPaymentPlanByIdHandler : IRequestHandler<GetPaymentPlanByIdCommand, GetPaymentPlanByIdResponse>
    {
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        private readonly ILogger<GetPaymentPlanByIdHandler> _logger;

        public GetPaymentPlanByIdHandler(
            IPaymentPlanRepository paymentPlanRepository,
            ILogger<GetPaymentPlanByIdHandler> logger)
        {
            _paymentPlanRepository = paymentPlanRepository;
            _logger = logger;
        }

        public async Task<GetPaymentPlanByIdResponse> Handle(GetPaymentPlanByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentPlan = await _paymentPlanRepository.GetByIdWithDetails(request.Id, cancellationToken);

                if (paymentPlan == null)
                {
                    throw new NotFoundException($"Plano de pagamento com ID {request.Id} não encontrado.");
                }

                var response = new GetPaymentPlanByIdResponse(
                    paymentPlan.Id,
                    paymentPlan.TotalValue,
                    paymentPlan.CreateDate,
                    paymentPlan.ModifyDate
                )
                {
                    FinancialManager = new FinancialManagerDto(
                        paymentPlan.FinancialManager.Id,
                        paymentPlan.FinancialManager.Name
                    ),
                    CostCenter = new Dtos.CostCenterDto(
                        paymentPlan.CostCenter.Id,
                        paymentPlan.CostCenter.Name
                    )
                };

                foreach (var billing in paymentPlan.Billings)
                {
                    response.Billings.Add(new BillingDto(
                        billing.Id,
                        billing.Value,
                        billing.DueDate,
                        billing.PaymentMethod.ToPortugueseString(),
                        billing.StatusBilling.ToPortugueseString(),
                        billing.PaymentCode ?? string.Empty,
                        billing.PaymentPlanId,
                        billing.CreateDate,
                        billing.ModifyDate
                    ));
                }

                _logger.LogInformation("Detalhes do plano de pagamento recuperados para o ID: {PaymentPlanId}", paymentPlan.Id);

                return response;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao recuperar detalhes do plano de pagamento para o ID {request.Id}";
                _logger.LogError(ex, msg,
                    request.Id);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}