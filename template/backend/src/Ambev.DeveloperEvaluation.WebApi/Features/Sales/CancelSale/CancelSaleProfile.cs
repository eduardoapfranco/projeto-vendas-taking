using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            CreateMap<CancelSaleCommand, CancelSaleRequest>(); 
            CreateMap<bool, CancelSaleResponse>()
                .ConvertUsing(result => new CancelSaleResponse { Cancelled = result });
        }
    }
}
