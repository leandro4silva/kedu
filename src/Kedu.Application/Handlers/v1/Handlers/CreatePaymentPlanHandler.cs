using Kedu.Application.Dtos;
using Kedu.Application.Exceptions;
using Kedu.Application.Extensions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class CreatePaymentPlanHandler : IRequestHandler<CreatePaymentPlanCommand, CreatePaymentPlanResponse>
    {
        private readonly IPaymentPlanRepository _paymentPlanRepository;
        private readonly IFinancialManagerRepository _financialManagerRepository;
        private readonly ICostCenterRepository _costCenterRepository;
        private readonly IBillingRepository _billingRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreatePaymentPlanHandler> _logger;

        public CreatePaymentPlanHandler(
            IPaymentPlanRepository paymentPlanRepository,
            IFinancialManagerRepository financialManagerRepository,
            ICostCenterRepository costCenterRepository,
            IBillingRepository billingRepository,
            IUnitOfWork unitOfWork,
            ILogger<CreatePaymentPlanHandler> logger)
        {
            _paymentPlanRepository = paymentPlanRepository;
            _financialManagerRepository = financialManagerRepository;
            _costCenterRepository = costCenterRepository;
            _billingRepository = billingRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<CreatePaymentPlanResponse> Handle(CreatePaymentPlanCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financialManager = await _financialManagerRepository.Get(request.FinancialManagerId, cancellationToken);
                if (financialManager == null)
                {
                    throw new NotFoundException($"Responsável financeiro com ID {request.FinancialManagerId} não encontrado.");
                }

                var costCenter = await _costCenterRepository.Get(request.CostCenterId, cancellationToken);
                if (costCenter == null)
                {
                    throw new NotFoundException($"Centro de custo com ID {request.CostCenterId} não encontrado.");
                }

                decimal totalValue = request.Billings.Sum(b => b.Value);

                var paymentPlan = new PaymentPlan(totalValue, request.FinancialManagerId, request.CostCenterId);

                await _paymentPlanRepository.Insert(paymentPlan, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

                foreach (var billingItem in request.Billings)
                {
                    var billing = new Billing(
                        billingItem.Value,
                        billingItem.DueDate,
                        billingItem.PaymentMethod,
                        paymentPlan.Id
                    );
                    paymentPlan.AddBilling(billing);

                    await _billingRepository.Insert(billing, cancellationToken);
                }

                await _unitOfWork.Commit(cancellationToken);

                var response = new CreatePaymentPlanResponse(
                    paymentPlan.Id,
                    paymentPlan.FinancialManagerId,
                    paymentPlan.CostCenterId,
                    paymentPlan.TotalValue,
                    paymentPlan.CreateDate
                );

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

                _logger.LogInformation("Plano de pagamento criado com o ID: {PaymentPlanId}", paymentPlan.Id);

                return response;
            }
            catch (NotFoundException)
            {
                throw;
            }
            catch (Exception ex)
            {
                var msg = $"Erro ao criar o plano de pagamento";
                _logger.LogError(ex, msg);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}