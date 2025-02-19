using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesProfile : Profile
    {
        public GetSalesProfile()
        {
            CreateMap<List<SaleDto>, GetSalesResponse>()
                .ConvertUsing(src => new GetSalesResponse { Sales = src });
        }
    }
}
