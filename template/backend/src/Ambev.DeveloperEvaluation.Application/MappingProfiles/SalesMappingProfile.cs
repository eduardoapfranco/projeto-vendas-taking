using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;

namespace Ambev.DeveloperEvaluation.Application.Sales.MappingProfiles
{
    public class SalesMappingProfile : Profile
    {
        public SalesMappingProfile()
        {
            // Mapeia a entidade Sale para o SaleDto
            CreateMap<Sale, SaleDto>()
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            // Mapeia a entidade SaleItem para o SaleItemDto
            CreateMap<SaleItem, SaleItemDto>();
        }
    }
}
