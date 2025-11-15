using Kedu.Application.Handlers.v1.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Controllers
{
    [ApiController]
    [Route("api/v1/planos-de-pagamento")]
    public class PaymentPlanController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PaymentPlanController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreatePaymentPlan([FromBody] CreatePaymentPlanCommand command)
        {
            var result = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetPaymentPlan), new { id = result.Id }, result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPaymentPlan(int id)
        {
            var query = new GetPaymentPlanByIdCommand(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/total")]
        public async Task<ActionResult> GetPaymentPlanTotal(int id)
        {
            var query = new GetPaymentPlanTotalCommand(id);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}