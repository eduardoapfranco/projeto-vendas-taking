using System;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Context;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.ORM
{
    public class SaleRepositoryTests
    {
        private DefaultContext CreateInMemoryContext()
        {
            var options = new DbContextOptionsBuilder<DefaultContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;
            return new DefaultContext(options);
        }

        [Fact]
        public async Task AddAndGetSaleAsync_ShouldWorkCorrectly()
        {
            // Arrange
            using var context = CreateInMemoryContext();
            var repository = new SaleRepository(context);
            var sale = Sale.Create("VENDA-TESTE", DateTime.UtcNow, "BR-001", "Filial Central", "CUST-001", "Cliente Teste");
            sale.AddItem(Guid.NewGuid(), "Produto A", 100, 5);

            // Act
            await repository.AddAsync(sale);
            await context.SaveChangesAsync();
            var fetchedSale = await repository.GetByIdAsync(sale.Id);

            // Assert
            Assert.NotNull(fetchedSale);
            Assert.Equal("VENDA-TESTE", fetchedSale.SaleNumber);
            Assert.Single(fetchedSale.Items);
        }
    }
}
