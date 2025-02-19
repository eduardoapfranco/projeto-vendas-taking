using MediatR;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommand : IRequest<string>
    {
        public string Id { get; set; }
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public string BranchId { get; set; }
        public string BranchName { get; set; }
        public string CustomerId { get; set; }
        public string CustomerName { get; set; }
        public List<CreateSaleItemDto> Items { get; set; }
    }

    public class CreateSaleItemDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
