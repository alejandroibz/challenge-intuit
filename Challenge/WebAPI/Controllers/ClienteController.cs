using Application.Features.Cliente.Commands;
using Application.Features.Cliente.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClienteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create([FromBody] CreateClientCommand request)
        {
            var result = await _mediator.Send(request);
            return Created("", new { result });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetById([FromQuery, Required] int id)
        {
            var query = new GetClientByIdQuery { Id = id };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll([FromQuery, Required] int page, [FromQuery, Required] int pageSize, [FromQuery] string? textToSearch)
        {
            var query = new GetAllClientsQuery { Page = page, PageSize = pageSize, TextToSearch = textToSearch };
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update([FromBody] UpdateClientCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpDelete("[action]")]
        public async Task<IActionResult> Delete([FromQuery, Required] int id)
        {
            var command = new DeleteClientCommand { ClientId = id };
            var result = await _mediator.Send(command);

            return Ok(result); 
        }
    }
}
