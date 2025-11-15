using Kedu.Application.Handlers.v1.Commands;
using Kedu.Application.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Controllers
{
    [ApiController]
    [Route("api/v1/cobrancas")]
    public class BillingController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BillingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{id}/pagamentos")]
        public async Task<ActionResult> RegisterPayment(int id, [FromBody] RegisterBillingPaymentCommand command)
        {
            command.BillingId = id;
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}