
using Kedu.Application.Handlers.v1.Commands;
using Kedu.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Kedu.Api.Controllers
{
    [ApiController]
    [Route("api/v1/responsaveis")]
    public class FinancialManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FinancialManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult> CreateFinancialManager(
            [FromBody] CreateFinancialManagerCommand request)
        {
            var result = await _mediator.Send(request);

            return CreatedAtAction(
                nameof(GetPaymentPlans),
                new { id = result.Id },
                result
            );
        }

        [HttpGet("{id}/planos-de-pagamento")]
        public async Task<ActionResult<GetPaymentPlansByFinancialManagerResponse>> GetPaymentPlans(int id)
        {
            var command = new GetPaymentPlansByFinancialManagerCommand();

            command.FinancialManagerId = id;

            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet("{id}/cobrancas")]
        public async Task<ActionResult<GetBillingsByFinancialManagerResponse>> GetBillings(
            int id,
            [FromQuery] StatusBilling? status = null,
            [FromQuery] PaymentMethod? metodoPagamento = null,
            [FromQuery] DateTime? dataInicio = null,
            [FromQuery] DateTime? dataFim = null)
        {
            var query = new GetBillingsByFinancialManagerCommand(id, status, metodoPagamento, dataInicio, dataFim);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}/cobrancas/quantidade")]
        public async Task<ActionResult<GetBillingCountByFinancialManagerResponse>> GetBillingCount(
            int id,
            [FromQuery] StatusBilling? status = null,
            [FromQuery] PaymentMethod? metodoPagamento = null)
        {
            var query = new GetBillingCountByFinancialManagerCommand(id, status, metodoPagamento);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}