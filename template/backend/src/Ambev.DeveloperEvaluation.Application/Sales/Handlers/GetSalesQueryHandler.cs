using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, PaginatedResult<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedResult<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            // Obtenha a lista completa de vendas
            var allSales = await _saleRepository.GetAllAsync();
            int totalCount = allSales.Count;

            // Aplique paginação na lista em memória
            var pagedSales = allSales
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .ToList();

            var saleDtos = _mapper.Map<List<SaleDto>>(pagedSales);

            return new PaginatedResult<SaleDto>
            {
                Items = saleDtos,
                TotalCount = totalCount,
                CurrentPage = request.PageNumber,
                TotalPages = (int)Math.Ceiling(totalCount / (double)request.PageSize)
            };
        }
    }
}
