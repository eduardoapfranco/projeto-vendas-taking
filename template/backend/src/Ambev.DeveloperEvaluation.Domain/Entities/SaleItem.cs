namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem
    {
        public string Id { get; private set; }
        public string ProductId { get; private set; }
        public string ProductName { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }
        public decimal DiscountPercentage { get; private set; }
        public decimal Total { get; private set; }
        public bool IsCancelled { get; private set; }

        // Construtor privado para EF
        private SaleItem() { }

        public SaleItem(string id, string productId, string productName,
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
