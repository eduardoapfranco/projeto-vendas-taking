using System;
using Xunit;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;

namespace Ambev.DeveloperEvaluation.Tests.Domain
{
    public class SaleTests
    {
        [Fact]
        public void AddItem_QuantityLessThan4_ShouldApplyZeroDiscount()
        {
            // Arrange
            var sale = Sale.Create("VENDA-001", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            // Act
            sale.AddItem(Guid.NewGuid(), "Produto A", 100, 3);

            // Assert: Verifica se o desconto é 0
            var item = Assert.Single(sale.Items);
            Assert.Equal(0, item.DiscountPercentage);
            // Total deve ser unitPrice * quantity
            Assert.Equal(100 * 3, item.Total);
        }

        [Fact]
        public void AddItem_QuantityBetween4And9_ShouldApply10PercentDiscount()
        {
            // Arrange
            var sale = Sale.Create("VENDA-002", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            // Act
            sale.AddItem(Guid.NewGuid(), "Produto B", 100, 5);

            // Assert
            var item = Assert.Single(sale.Items);
            Assert.Equal(0.10m, item.DiscountPercentage);
            var expectedTotal = (100 * 5) - (100 * 5 * 0.10m);
            Assert.Equal(expectedTotal, item.Total);
        }

        [Fact]
        public void AddItem_QuantityBetween10And20_ShouldApply20PercentDiscount()
        {
            // Arrange
            var sale = Sale.Create("VENDA-003", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            // Act
            sale.AddItem(Guid.NewGuid(), "Produto C", 100, 15);

            // Assert
            var item = Assert.Single(sale.Items);
            Assert.Equal(0.20m, item.DiscountPercentage);
            var expectedTotal = (100 * 15) - (100 * 15 * 0.20m);
            Assert.Equal(expectedTotal, item.Total);
        }

        [Fact]
        public void AddItem_QuantityGreaterThan20_ShouldThrowDomainException()
        {
            // Arrange
            var sale = Sale.Create("VENDA-004", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");

            // Act & Assert
            Assert.Throws<DomainException>(() =>
            {
                sale.AddItem(Guid.NewGuid(), "Produto D", 100, 25);
            });
        }
    }
}
