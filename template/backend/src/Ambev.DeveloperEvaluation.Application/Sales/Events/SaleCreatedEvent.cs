namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleCreatedEvent
    {
        public string SaleId { get; }
        public string SaleNumber { get; }
        public DateTime CreatedAt { get; }

        public SaleCreatedEvent(string saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
