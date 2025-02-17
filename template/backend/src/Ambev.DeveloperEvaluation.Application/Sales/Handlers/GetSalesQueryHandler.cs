using MediatR;
using AutoMapper;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class GetSalesQueryHandler : IRequestHandler<GetSalesQuery, List<SaleDto>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<List<SaleDto>> Handle(GetSalesQuery request, CancellationToken cancellationToken)
        {
            var allSales = await _saleRepository.GetAllAsync();
            var pagedSales = allSales
                .Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize);

            return _mapper.Map<List<SaleDto>>(pagedSales);
        }
    }
}
