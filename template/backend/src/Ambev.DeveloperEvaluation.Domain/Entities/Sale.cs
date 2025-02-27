namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale
    {
        public string Id { get; private set; }
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string BranchId { get; private set; }
        public string BranchName { get; private set; }
        public string CustomerId { get; private set; }
        public string CustomerName { get; private set; }
        public bool IsCancelled { get; private set; }
        private List<SaleItem> _items;
        public IReadOnlyCollection<SaleItem> Items => _items;

        // Construtor para EF
        private Sale()
        {
            _items = new List<SaleItem>();
        }

        public static Sale Create(string saleNumber, DateTime saleDate,
                                  string branchId, string branchName,
                                  string customerId, string customerName)
        {
            return new Sale
            {
                Id = Guid.NewGuid().ToString(), 
                SaleNumber = saleNumber,
                SaleDate = saleDate,
                BranchId = branchId,
                BranchName = branchName,
                CustomerId = customerId,
                CustomerName = customerName,
                IsCancelled = false
            };
        }

        public void AddItem(string productId, string productName, decimal unitPrice, int quantity)
        {
            if (quantity > 20)
                throw new DomainException("Não é permitido vender mais de 20 itens iguais.");

            decimal discountPercentage = 0;
            if (quantity >= 10) discountPercentage = 0.20m;
            else if (quantity >= 4) discountPercentage = 0.10m;

            decimal discountValue = (unitPrice * quantity) * discountPercentage;
            decimal total = (unitPrice * quantity) - discountValue;
            var newItem = new SaleItem(Guid.NewGuid().ToString(), productId, productName, unitPrice, quantity, discountPercentage, total);
            _items.Add(newItem);
        }
        // Novo método para atualizar a venda
        public void UpdateSale(string idVenda, string saleNumber, DateTime saleDate, string branchId, string branchName, string customerId, string customerName)
        {
            if (IsCancelled)
                throw new DomainException("Não é possível atualizar uma venda cancelada.");
            Id = idVenda;
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            BranchId = branchId;
            BranchName = branchName;
            CustomerId = customerId;
            CustomerName = customerName;
        }

        public void CancelSale()
        {
            IsCancelled = true;
        }
    }
}