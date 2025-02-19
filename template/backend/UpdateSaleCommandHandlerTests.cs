using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Application.Sales.Handlers;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Application.Sales
{
    public class UpdateSaleCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UpdateSaleCommandHandler _handler;

        public UpdateSaleCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            var config = new MapperConfiguration(cfg =>
            {
                // Configure o mapeamento entre UpdateSaleRequest e UpdateSaleCommand se houver um profile.
                cfg.CreateMap<UpdateSaleRequest, UpdateSaleCommand>();
            });
            _mapper = config.CreateMapper();

            _handler = new UpdateSaleCommandHandler(_saleRepository, _unitOfWork, _mapper);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldUpdateSaleAndReturnTrue()
        {
            // Arrange: Crie um ID de venda e simule uma venda existente.
            var saleId = Guid.NewGuid().ToString();
            var existingSale = Sale.Create("VENDA-OLD", DateTime.UtcNow.AddDays(-1), "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            // Configure o repositório para retornar a venda existente.
            _saleRepository.GetByIdAsync(saleId).Returns(existingSale);
            _saleRepository.UpdateAsync(Arg.Any<Sale>()).Returns(Task.CompletedTask);
            _unitOfWork.CommitAsync().Returns(Task.CompletedTask);

            var command = new UpdateSaleCommand
            {
                SaleId = saleId,
                SaleNumber = "VENDA-UPDATED",
                SaleDate = DateTime.UtcNow,
                BranchId = "BR-002",
                BranchName = "Filial Atualizada",
                CustomerId = "CUST-002",
                CustomerName = "Cliente Atualizado"
            };

            // Act: Processa o comando.
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert: Verifica se o resultado é verdadeiro e se os métodos foram chamados.
            Assert.True(result);
            await _saleRepository.Received(1).UpdateAsync(Arg.Is<Sale>(s => s.SaleNumber == "VENDA-UPDATED"));
            await _unitOfWork.Received(1).CommitAsync();
        }
    }
}
