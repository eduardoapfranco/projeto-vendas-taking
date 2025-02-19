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
    public class CancelSaleCommandHandlerTests
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly CancelSaleCommandHandler _handler;

        public CancelSaleCommandHandlerTests()
        {
            _saleRepository = Substitute.For<ISaleRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();

            _handler = new CancelSaleCommandHandler(_saleRepository, _unitOfWork);
        }

        [Fact]
        public async Task Handle_ValidCommand_ShouldCancelSaleAndReturnTrue()
        {
            // Arrange
            var saleId = Guid.NewGuid().ToString();
            var existingSale = Sale.Create("VENDA-TESTE", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");
            // Certifique-se de que a venda pode ser cancelada.
            _saleRepository.GetByIdAsync(saleId).Returns(existingSale);
            _unitOfWork.CommitAsync().Returns(Task.CompletedTask);

            var command = new CancelSaleCommand { SaleId = saleId };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result);
            // Verifica se a venda foi cancelada 
            Assert.True(existingSale.IsCancelled);
            await _unitOfWork.Received(1).CommitAsync();
        }
    }
}
