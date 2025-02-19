namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleUpdatedEvent
    {
        public string SaleId { get; }
        public string SaleNumber { get; }
        public DateTime UpdatedAt { get; }

        public SaleUpdatedEvent(string saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            UpdatedAt = DateTime.UtcNow;
        }
    }
}
