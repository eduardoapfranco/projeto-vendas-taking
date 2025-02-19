using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Application.Sales
{
    public class CreateSaleCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly CreateSaleCommandHandler _handler;

        public CreateSaleCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale.CreateSaleProfile>();
            });
            _mapper = config.CreateMapper();

            _handler = new CreateSaleCommandHandler(_saleRepository, _unitOfWork, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCreateSaleAndReturnId()
        {
            // Arrange: Comando com dados válidos
            var command = new CreateSaleCommand
            {
                SaleNumber = "VENDA-TESTE",
                SaleDate = DateTime.UtcNow,
                BranchId = "BR-001",
                BranchName = "Filial Central",
                CustomerId = "CUST-001",
                CustomerName = "Cliente Teste",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        ProductId = "PROD-001",
                        ProductName = "Produto Teste",
                        UnitPrice = 100,
                        Quantity = 5
                    }
                }
            };

            _saleRepository.AddAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);
            _unitOfWork.CommitAsync().Returns(Task.CompletedTask);

            // Act
            var saleId = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(string.IsNullOrEmpty(saleId));
            await _saleRepository.Received(1).AddAsync(Arg.Any<Sale>());
            await _unitOfWork.Received(1).CommitAsync();
        }

        [Fact]
        public async Task Handle_CommandWithItemQuantityGreaterThan20_ShouldThrowDomainException()
        {
            // Arrange: Crie um comando com um item cuja quantidade é maior que 20 (inválida)
            var command = new CreateSaleCommand
            {
                SaleNumber = "VENDA-INVALIDA",
                SaleDate = DateTime.UtcNow,
                BranchId = "BR-001",
                BranchName = "Filial Central",
                CustomerId = "CUST-001",
                CustomerName = "Cliente Teste",
                Items = new List<CreateSaleItemDto>
                {
                    new CreateSaleItemDto
                    {
                        ProductId = "PROD-002",
                        ProductName = "Produto Excesso",
                        UnitPrice = 100,
                        Quantity = 25  // Quantidade inválida (>20)
                    }
                }
            };

            // Act & Assert: Espera que seja lançada uma DomainException
            await Assert.ThrowsAsync<DomainException>(async () =>
            {
                await _handler.Handle(command, CancellationToken.None);
            });
        }
    }
}
