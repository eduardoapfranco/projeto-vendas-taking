using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        // POST: api/sales
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSaleRequest request)
        {
            if (request == null)
                return BadRequest("Requisição inválida.");

            // Mapeia o request para o comando de criação (definido na camada Application)
            var command = _mapper.Map<CreateSaleCommand>(request);
            string saleId = await _mediator.Send(command);
            var response = _mapper.Map<CreateSaleResponse>(saleId);
            return Ok(response);
        }

        // GET: api/sales/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var query = new GetSaleByIdQuery { SaleId = id };
            var saleDto = await _mediator.Send(query);
            if (saleDto == null)
                return NotFound();
            return Ok(saleDto);
        }

        // GET: api/sales
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetSalesQuery query)
        {
            var salesList = await _mediator.Send(query);
            return Ok(salesList);
        }


        // PUT: api/sales/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateSaleRequest request)
        {
            if (request == null)
                return BadRequest("Requisição inválida.");

            // Define o id da venda no request
            request.SaleId = id;
            var command = _mapper.Map<UpdateSaleCommand>(request);
            bool result = await _mediator.Send(command);
            return Ok(new { Updated = result });
        }

        // PUT: api/sales/{id}/cancel
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> Cancel(string id)
        {
            var command = new CancelSaleCommand { SaleId = id };
            bool result = await _mediator.Send(command);
            return Ok(new { Cancelled = result });
        }
    }
}
