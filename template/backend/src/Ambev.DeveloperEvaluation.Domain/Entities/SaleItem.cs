namespace Ambev.DeveloperEvaluation.Domain.Entities;
{
    public class SaleItem
    {
        public Guid Id { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal Total { get; private set; }
        public bool IsCancelled { get; private set; }

        // Construtor privado para EF
        private SaleItem() { }

        public SaleItem(Guid id, Guid productId, string productName,
                        decimal unitPrice, int quantity,
                        decimal discountPercentage, decimal total)
        {
            Id = id;
            ProductId = productId;
            ProductName = productName;
            UnitPrice = unitPrice;
            Quantity = quantity;
            DiscountPercentage = discountPercentage;
            Total = total;
            IsCancelled = false;
        }

        public void CancelItem()
        {
            IsCancelled = true;
        }
    }
}
