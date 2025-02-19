using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.WebApi;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Tests.Integration
{
    public class SalesIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public SalesIntegrationTests(WebApplicationFactory<Program> factory)
        {
            // Configuração para usar InMemory ou outra configuração de teste no backend
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateSale_ValidData_ReturnsCreatedSaleId()
        {
            // Arrange
            var request = new
            {
                saleNumber = "VENDA-INTEG-001",
                saleDate = "2025-02-20T00:00:00Z",
                branchId = "BR-001",
                branchName = "Filial Teste",
                customerId = "CUST-001",
                customerName = "Cliente Teste",
                items = new[]
                {
                    new { productId = "PROD-001", productName = "Produto Teste", unitPrice = 100, quantity = 5 }
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/Sales", content);

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var responseBody = await response.Content.ReadAsStringAsync();
            Assert.Contains("saleId", responseBody);
        }

        [Fact]
        public async Task UpdateSale_InvalidData_ReturnsBadRequest()
        {
            // Arrange
            // Tente atualizar uma venda com dados inválidos (ex: quantidade > 20)
            var saleId = "406372bf-9693-4522-8727-2e3e7020a975";
            var request = new
            {
                saleId = saleId,
                saleNumber = "VENDA-UPDATE",
                saleDate = "2025-02-21T00:00:00Z",
                branchId = "BR-002",
                branchName = "Filial Update",
                customerId = "CUST-002",
                customerName = "Cliente Update",
                items = new[]
                {
                    new { productId = "PROD-002", productName = "Produto Excesso", unitPrice = 100, quantity = 25 } // Inválido
                }
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PutAsync($"/api/Sales/{saleId}", content);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }
    }
}
