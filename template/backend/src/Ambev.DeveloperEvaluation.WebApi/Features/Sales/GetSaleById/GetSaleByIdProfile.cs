using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdProfile : Profile
    {
        public GetSaleByIdProfile()
        {
            CreateMap<SaleDto, GetSaleByIdResponse>();
        }
    }
}
