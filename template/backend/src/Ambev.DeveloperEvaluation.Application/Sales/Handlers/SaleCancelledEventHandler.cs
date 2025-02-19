using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class SaleCancelledEventHandler : IHandleMessages<SaleCancelledEvent>
    {
        public async Task Handle(SaleCancelledEvent message)
        {
            Console.WriteLine($"[SaleCancelledEvent] Venda cancelada: {message.SaleId} - {message.SaleNumber}");
            await Task.CompletedTask;
        }
    }
}
