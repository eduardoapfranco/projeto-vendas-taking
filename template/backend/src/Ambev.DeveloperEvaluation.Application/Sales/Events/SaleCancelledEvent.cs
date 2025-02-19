namespace Ambev.DeveloperEvaluation.Application.Sales.Events
{
    public class SaleCancelledEvent
    {
        public string SaleId { get; }
        public string SaleNumber { get; }
        public DateTime CancelledAt { get; }

        public SaleCancelledEvent(string saleId, string saleNumber)
        {
            SaleId = saleId;
            SaleNumber = saleNumber;
            CancelledAt = DateTime.UtcNow;
        }
    }
}
