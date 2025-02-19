using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class SaleUpdatedEventHandler : IHandleMessages<SaleUpdatedEvent>
    {
        public async Task Handle(SaleUpdatedEvent message)
        {
            Console.WriteLine($"[SaleUpdatedEvent] Venda atualizada: {message.SaleId} - {message.SaleNumber}");
            await Task.CompletedTask;
        }
    }
}
