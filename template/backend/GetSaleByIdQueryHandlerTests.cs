using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Queries;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Application.Sales
{
    public class GetSaleByIdQueryHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly GetSaleByIdQueryHandler _handler;

        public GetSaleByIdQueryHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();

            var config = new MapperConfiguration(cfg =>
            {
                // Configure o mapeamento de Sale para SaleDto
                cfg.CreateMap<Sale, SaleDto>();
                cfg.CreateMap<SaleItem, SaleItemDto>();
            });
            _mapper = config.CreateMapper();

            _handler = new GetSaleByIdQueryHandler(_saleRepository, _mapper);
        }

        [Fact]
        public async Task Handle_ExistingSale_ShouldReturnSaleDto()
        {
            // Arrange
            var saleId = Guid.NewGuid().ToString();
            var existingSale = Sale.Create("VENDA-TESTE", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            _saleRepository.GetByIdAsync(saleId).Returns(existingSale);

            var query = new GetSaleByIdQuery { SaleId = saleId };

            // Act
            var saleDto = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(saleDto);
            Assert.Equal(existingSale.SaleNumber, saleDto.SaleNumber);
        }
    }
}
