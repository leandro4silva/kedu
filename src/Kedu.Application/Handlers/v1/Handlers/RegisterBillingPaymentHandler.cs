using Kedu.Application.Exceptions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Exceptions;
using Kedu.Domain.Repositories;
using Kedu.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class RegisterBillingPaymentHandler : IRequestHandler<RegisterBillingPaymentCommand, RegisterBillingPaymentResponse>
    {
        private readonly IBillingRepository _billingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<RegisterBillingPaymentHandler> _logger;

        public RegisterBillingPaymentHandler(
            IBillingRepository billingRepository,
            IUnitOfWork unitOfWork,
            ILogger<RegisterBillingPaymentHandler> logger)
        {
            _billingRepository = billingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<RegisterBillingPaymentResponse> Handle(RegisterBillingPaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var billing = await _billingRepository.Get(request.BillingId, cancellationToken);
                if (billing == null)
                {
                    throw new NotFoundException($"Cobrança com ID {request.BillingId} não encontrada.");
                }

                billing.RegisterPayment(request.Value, request.PaymentDate);

                await _billingRepository.Update(billing, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

                _logger.LogInformation("Pagamento registrado para a cobrança com ID: {BillingId}", billing.Id);

                return new RegisterBillingPaymentResponse(
                    billing.Id,
                    billing.Value,
                    billing.PaidValue ?? 0,
                    billing.DueDate,
                    billing.PaymentDate ?? DateTime.MinValue,
                    billing.PaymentMethod.ToString(),
                    billing.StatusBilling.ToString(),
                    billing.PaymentCode ?? string.Empty,
                    billing.PaymentPlanId,
                    billing.CreateDate,
                    billing.ModifyDate
                );
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (EntityValidationException ex)
            {
                var msg = $"Erro de domínio ao registrar o pagamento para o ID de cobrança {request.BillingId}";
                _logger.LogWarning(ex, msg, request.BillingId);
                throw new BadRequestException(ex.Message);
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao registrar o pagamento para o ID de cobrança {request.BillingId}";
                _logger.LogError(ex, msg, request.BillingId);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}