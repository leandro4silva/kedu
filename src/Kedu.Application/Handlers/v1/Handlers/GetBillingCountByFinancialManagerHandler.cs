using Kedu.Application.Dtos;
using Kedu.Application.Exceptions;
using Kedu.Application.Extensions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetBillingCountByFinancialManagerHandler : IRequestHandler<GetBillingCountByFinancialManagerCommand, GetBillingCountByFinancialManagerResponse>
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IFinancialManagerRepository _financialManagerRepository;
        private readonly ILogger<GetBillingCountByFinancialManagerHandler> _logger;

        public GetBillingCountByFinancialManagerHandler(
            IBillingRepository billingRepository,
            IFinancialManagerRepository financialManagerRepository,
            ILogger<GetBillingCountByFinancialManagerHandler> logger)
        {
            _billingRepository = billingRepository;
            _financialManagerRepository = financialManagerRepository;
            _logger = logger;
        }

        public async Task<GetBillingCountByFinancialManagerResponse> Handle(GetBillingCountByFinancialManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financialManager = await _financialManagerRepository.Get(request.FinancialManagerId, cancellationToken);
                if (financialManager == null)
                {
                    throw new NotFoundException($"Responsável financeiro com ID {request.FinancialManagerId} não encontrado.");
                }

                var count = await _billingRepository.GetCountByFinancialManagerId(
                    request.FinancialManagerId,
                    request.Status,
                    request.PaymentMethod,
                    cancellationToken);

                var statusDescription = request.Status?.ToPortugueseString() ?? "TODOS";
                var paymentMethodDescription = request.PaymentMethod?.ToPortugueseString() ?? "TODOS";

                var filters = new BillingFiltersDto(
                    statusDescription,
                    paymentMethodDescription
                );

                _logger.LogInformation("Contagem de faturamento recuperada {Count} para o gerente financeiro com ID {FinancialManagerId}",
                    count, request.FinancialManagerId);

                return new GetBillingCountByFinancialManagerResponse(
                    financialManager.Id,
                    financialManager.Name,
                    count,
                    filters
                );
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao recuperar a contagem de faturamento para o gerente financeiro com ID {request.FinancialManagerId}";
                _logger.LogError(ex, msg,
                    request.FinancialManagerId);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}