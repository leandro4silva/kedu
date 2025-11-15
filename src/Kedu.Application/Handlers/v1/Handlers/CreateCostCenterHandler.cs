using Kedu.Application.Exceptions;
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Entities;
using Kedu.Domain.Repositories;
using Kedu.Domain.SeedWork;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class CreateCostCenterHandler : IRequestHandler<CreateCostCenterCommand, CreateCostCenterResponse>
    {
        private readonly ICostCenterRepository _repository;
        private readonly ILogger<CreateCostCenterHandler> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCostCenterHandler(
            ICostCenterRepository repository,
            ILogger<CreateCostCenterHandler> logger,
            IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateCostCenterResponse> Handle(CreateCostCenterCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var costCenter = new CostCenter(request.Name);

                await _repository.Insert(costCenter, cancellationToken);
                await _unitOfWork.Commit(cancellationToken);

                _logger.LogInformation("Centro de custos criado com o ID: {Id}", costCenter.Id);

                return new CreateCostCenterResponse(
                    costCenter.Id,
                    costCenter.Name,
                    costCenter.CreateDate
                );
            }
            catch (Exception ex)
            {
                var msg = "Error creating cost center";
                _logger.LogError(ex, msg);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}