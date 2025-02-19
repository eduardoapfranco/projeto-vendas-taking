using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class UpdateSaleCommand : IRequest<bool>
    {
        public string SaleId { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
    }
}
