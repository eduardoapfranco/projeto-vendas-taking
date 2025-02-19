using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    public class CancelSaleProfile : Profile
    {
        public CancelSaleProfile()
        {
            // Mapeia o request para o comando
            CreateMap<CancelSaleRequest, CancelSaleCommand>();

            // Mapeia o resultado booleano para o response
            CreateMap<bool, CancelSaleResponse>()
                .ConvertUsing(result => new CancelSaleResponse { Cancelled = result });
        }
    }
}
