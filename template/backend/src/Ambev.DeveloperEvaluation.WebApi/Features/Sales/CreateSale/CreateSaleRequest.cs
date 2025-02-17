using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
