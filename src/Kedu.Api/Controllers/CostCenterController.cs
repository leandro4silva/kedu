using Kedu.Application.Handlers.v1.Commands;
using Kedu.Application.Handlers.v1.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Controllers
{
    [ApiController]
    [Route("api/v1/centros-de-custo")]
    public class CostCenterController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CostCenterController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<CreateCostCenterResponse>> CreateCostCenter([FromBody] CreateCostCenterCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCostCenters), result);
        }

        [HttpGet]
        public async Task<ActionResult<GetAllCostCentersResponse>> GetCostCenters()
        {
            var query = new GetAllCostCentersCommand();
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}