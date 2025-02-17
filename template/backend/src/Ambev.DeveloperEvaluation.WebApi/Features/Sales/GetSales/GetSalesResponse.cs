using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesResponse
    {
        public List<SaleDto> Sales { get; set; }
    }
}
