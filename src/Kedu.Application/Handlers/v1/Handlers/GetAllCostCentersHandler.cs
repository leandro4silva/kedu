using Kedu.Application.Exceptions;
using Kedu.Application.Handlers.v1.Queries;
using Kedu.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Kedu.Application.Handlers.v1.Handlers
{
    public class GetAllCostCentersHandler : IRequestHandler<GetAllCostCentersCommand, GetAllCostCentersResponse>
    {
        private readonly ICostCenterRepository _repository;
        private readonly ILogger<GetAllCostCentersHandler> _logger;

        public GetAllCostCentersHandler(
            ICostCenterRepository repository,
            ILogger<GetAllCostCentersHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<GetAllCostCentersResponse> Handle(GetAllCostCentersCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var costCenters = await _repository.GetAll(cancellationToken);

                var costCenterDtos = costCenters.Select(cc => new CostCenterDto(
                    cc.Id,
                    cc.Name,
                    cc.CreateDate,
                    cc.ModifyDate
                )).ToList();

                _logger.LogInformation("Recuperados {Count} centros de custo", costCenterDtos.Count);

                return new GetAllCostCentersResponse(costCenterDtos, costCenterDtos.Count);
            }
            catch (Exception ex)
            {
                var msg = "Erro ao recuperar centros de custo";
                _logger.LogError(ex, msg);
                throw new InternalServerErrorException(msg);
            }
        }
    }
}