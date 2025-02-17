using MediatR;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public class GetSalesQuery : IRequest<List<SaleDto>>
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
