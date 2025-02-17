

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public Guid Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid BranchId { get; private set; }
        public string BranchName { get; private set; }
        public Guid CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public bool IsCancelled { get; private set; }
        private readonly List<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items => _items;

        private Sale()
        {
            _items = new List<SaleItem>();
        }

        public static Sale Create(string saleNumber, DateTime saleDate,
                                  Guid branchId, string branchName,
                                  Guid customerId, string customerName)
        {
            return new Sale
            {
                Id = Guid.NewGuid(),
                SaleNumber = saleNumber,
                SaleDate = saleDate,
                BranchId = branchId,
                BranchName = branchName,
                CustomerId = customerId,
                CustomerName = customerName,
                IsCancelled = false
            };
        }

        public void AddItem(Guid productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity > 20)
                throw new DomainException("Não é permitido vender mais de 20 itens iguais.");

            decimal discountPercentage = 0m;
            if (quantity >= 10)
                discountPercentage = 0.20m;
            else if (quantity >= 4)
                discountPercentage = 0.10m;

            decimal discountValue = unitPrice * quantity * discountPercentage;
            decimal total = (unitPrice * quantity) - discountValue;

            var item = new SaleItem(Guid.NewGuid(), productId, productName, unitPrice, quantity, discountPercentage, total);
            _items.Add(item);
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }

        // Novo método para atualizar a venda
        public void UpdateSale(string saleNumber, DateTime saleDate, Guid branchId, string branchName, Guid customerId, string customerName)
        {
            if (IsCancelled)
                throw new DomainException("Não é possível atualizar uma venda cancelada.");

            SaleNumber = saleNumber;
            SaleDate = saleDate;
            BranchId = branchId;
            BranchName = branchName;
            CustomerId = customerId;
            CustomerName = customerName;
        }
    }
}
