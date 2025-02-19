using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            // Mapeia o request para o comando
            CreateMap<CreateSaleRequest, CreateSaleCommand>();

            // Adicione o mapeamento para os itens:
            CreateMap<SaleItemDto, CreateSaleItemDto>();

            CreateMap<string, CreateSaleResponse>()
                .ConvertUsing(saleId => new CreateSaleResponse { SaleId = saleId });
        }
    }
}
