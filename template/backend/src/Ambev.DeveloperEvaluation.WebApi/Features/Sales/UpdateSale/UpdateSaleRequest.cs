using System;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public Guid SaleId { get; set; } 
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public Guid BranchId { get; set; }
        public string BranchName { get; set; }
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
