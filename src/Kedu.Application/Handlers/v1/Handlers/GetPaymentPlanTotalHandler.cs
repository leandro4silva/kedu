using Kedu.Application.Exceptions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Application.Handlers.v1.Queries;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetPaymentPlanTotalHandler : IRequestHandler<GetPaymentPlanTotalCommand, GetPaymentPlanTotalResponse>
    {
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        private readonly ILogger<GetPaymentPlanTotalHandler> _logger;

        public GetPaymentPlanTotalHandler(
            IPaymentPlanRepository paymentPlanRepository,
            ILogger<GetPaymentPlanTotalHandler> logger)
        {
            _paymentPlanRepository = paymentPlanRepository;
            _logger = logger;
        }

        public async Task<GetPaymentPlanTotalResponse> Handle(GetPaymentPlanTotalCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var paymentPlan = await _paymentPlanRepository.GetByIdWithDetails(request.Id, cancellationToken);

                if (paymentPlan == null)
                {
                    throw new NotFoundException($"Plano de pagamento com ID {request.Id} não encontrado.");
                }

                var response = new GetPaymentPlanTotalResponse(
                    paymentPlan.Id,
                    paymentPlan.TotalValue,
                    paymentPlan.Billings?.Count ?? 0
                );

                _logger.LogInformation("Retrieved payment plan total for ID: {PaymentPlanId}", paymentPlan.Id);

                return response;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {

                var msg = $"Erro ao recuperar o total do plano de pagamento para o ID {request.Id}";
                _logger.LogError(ex, msg,
                    request.Id);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}