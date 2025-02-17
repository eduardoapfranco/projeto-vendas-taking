using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale; 

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            // Mapeia o request para o comando que será manipulado pelo MediatR
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

            // Converte o Guid retornado pelo handler para o response
            CreateMap<Guid, CreateSaleResponse>().ConvertUsing(saleId => new CreateSaleResponse { SaleId = saleId });
        }
    }
}
