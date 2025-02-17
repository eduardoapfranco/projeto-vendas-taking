using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleProfile : Profile
    {
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            CreateMap<bool, UpdateSaleResponse>()
                .ConvertUsing(result => new UpdateSaleResponse { Updated = result });
        }
    }
}
