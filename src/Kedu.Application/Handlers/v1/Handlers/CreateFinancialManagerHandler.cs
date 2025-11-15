using Kedu.Application.Exceptions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class CreateFinancialManagerHandler : IRequestHandler<CreateFinancialManagerCommand, CreateFinancialManagerResponse>
    {
        private readonly IFinancialManagerRepository _repository;
        private readonly ILogger<CreateFinancialManagerHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateFinancialManagerHandler(
            IFinancialManagerRepository repository,
            ILogger<CreateFinancialManagerHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateFinancialManagerResponse> Handle(CreateFinancialManagerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var financialManager = new FinancialManager(request.Name!);

                await _repository.Insert(financialManager, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

                _logger.LogInformation("Responsável financeiro criado com ID: {Id}", financialManager.Id);

                return new CreateFinancialManagerResponse(
                    financialManager.Id,
                    financialManager.Name,
                    financialManager.CreateDate
                );
            }
            catch (Exception ex)
            {
                var msg = "Error ao criar responsável financeiro";
                _logger.LogError(ex, msg);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}