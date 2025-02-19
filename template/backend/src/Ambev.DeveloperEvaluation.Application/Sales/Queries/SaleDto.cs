using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries
{
    public class SaleDto
    {
        public string Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public bool IsCancelled { get; set; }
        public List<SaleItemDto> Items { get; set; }
    }

    public class SaleItemDto
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal DiscountPercentage { get; set; }
        public decimal Total { get; set; }
        public bool IsCancelled { get; set; }
    }
}
