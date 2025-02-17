using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public class GetSaleByIdQuery : IRequest<SaleDto>
    {
        public string SaleId { get; set; }
    }
}
