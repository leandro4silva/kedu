using Kedu.Application.Dtos;
using Kedu.Application.Exceptions;
using Kedu.Application.Extensions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetBillingsByFinancialManagerHandler : IRequestHandler<GetBillingsByFinancialManagerCommand, GetBillingsByFinancialManagerResponse>
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IFinancialManagerRepository _financialManagerRepository;
        private readonly ILogger<GetBillingsByFinancialManagerHandler> _logger;

        public GetBillingsByFinancialManagerHandler(
            IBillingRepository billingRepository,
            IFinancialManagerRepository financialManagerRepository,
            ILogger<GetBillingsByFinancialManagerHandler> logger)
        {
            _billingRepository = billingRepository;
            _financialManagerRepository = financialManagerRepository;
            _logger = logger;
        }

        public async Task<GetBillingsByFinancialManagerResponse> Handle(GetBillingsByFinancialManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financialManager = await _financialManagerRepository.Get(request.FinancialManagerId, cancellationToken);
                if (financialManager == null)
                {
                    throw new NotFoundException($"Responsável financeiro com ID {request.FinancialManagerId} não encontrado.");
                }

                var billings = await _billingRepository.GetByFinancialManagerId(
                    request.FinancialManagerId,
                    request.Status,
                    request.PaymentMethod,
                    request.StartDate,
                    request.EndDate,
                    cancellationToken);

                var billingDtos = billings.Select(b => new BillingDto(
                    b.Id,
                    b.Value,
                    b.DueDate,
                    b.PaymentMethod.ToPortugueseString(),
                    b.StatusBilling.ToPortugueseString(),
                    b.PaymentCode ?? string.Empty,
                    b.PaymentPlanId,
                    b.CreateDate,
                    b.ModifyDate
                )).ToList();

                _logger.LogInformation("Recuperadas {Count} faturas para o gerente financeiro com ID {FinancialManagerId}",
                    billingDtos.Count, request.FinancialManagerId);

                return new GetBillingsByFinancialManagerResponse(
                    financialManager.Id,
                    financialManager.Name,
                    billingDtos,
                    billingDtos.Count
                );
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao recuperar faturas para o gerente financeiro com ID {request.FinancialManagerId}";
                _logger.LogError(ex, msg,
                    request.FinancialManagerId);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}