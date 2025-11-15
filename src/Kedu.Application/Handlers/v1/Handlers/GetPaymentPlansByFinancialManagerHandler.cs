using Kedu.Application.Dtos;
using Kedu.Application.Exceptions;
using Kedu.Application.Extensions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetPaymentPlansByFinancialManagerHandler : IRequestHandler<GetPaymentPlansByFinancialManagerCommand, GetPaymentPlansByFinancialManagerResponse>
    {
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        private readonly IFinancialManagerRepository _financialManagerRepository;
        private readonly ILogger<GetPaymentPlansByFinancialManagerHandler> _logger;

        public GetPaymentPlansByFinancialManagerHandler(
            IPaymentPlanRepository paymentPlanRepository,
            IFinancialManagerRepository financialManagerRepository,
            ILogger<GetPaymentPlansByFinancialManagerHandler> logger)
        {
            _paymentPlanRepository = paymentPlanRepository;
            _financialManagerRepository = financialManagerRepository;
            _logger = logger;
        }

        public async Task<GetPaymentPlansByFinancialManagerResponse> Handle(GetPaymentPlansByFinancialManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financialManager = await _financialManagerRepository.Get(request.FinancialManagerId, cancellationToken);
                if (financialManager == null)
                {
                    var msg = $"Responsável financeiro com ID {request.FinancialManagerId} não encontrado";
                    _logger.LogWarning(msg, request.FinancialManagerId);
                    throw new NotFoundException(msg);
                }

                var paymentPlans = await _paymentPlanRepository.GetByFinancialManagerId(request.FinancialManagerId, cancellationToken);

                var paymentPlanDtos = paymentPlans.Select(pp => new PaymentPlanDto(
                    pp.Id,
                    pp.TotalValue,
                    pp.FinancialManagerId,
                    pp.CreateDate,
                    pp.ModifyDate
                )
                {
                    Billings = pp.Billings?.Select(b => new BillingDto(
                        b.Id,
                        b.Value,
                        b.DueDate,
                        b.PaymentMethod.ToPortugueseString(),
                        b.StatusBilling.ToPortugueseString(),
                        b.PaymentCode ?? string.Empty,
                        b.PaymentPlanId,
                        b.CreateDate,
                        b.ModifyDate
                    )).ToList() ?? new List<BillingDto>()
                }).ToList();

                _logger.LogInformation("Recuperados {Count} planos de pagamento para o gerente financeiro com ID {FinancialManagerId}",
                    paymentPlanDtos.Count, request.FinancialManagerId);

                return new GetPaymentPlansByFinancialManagerResponse(financialManager.Id, financialManager.Name, paymentPlanDtos);
            }
            catch(NotFoundException ex)
            {
                throw; 
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao recuperar planos de pagamento para o gerente financeiro com ID {request.FinancialManagerId}";
                _logger.LogError(ex, msg,
                    request.FinancialManagerId);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}