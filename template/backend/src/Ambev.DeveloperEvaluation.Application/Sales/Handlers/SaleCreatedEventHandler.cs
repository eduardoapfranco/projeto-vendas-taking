using Rebus.Handlers;
using Ambev.DeveloperEvaluation.Application.Sales.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class SaleCreatedEventHandler : IHandleMessages<SaleCreatedEvent>
    {
        public async Task Handle(SaleCreatedEvent message)
        {
           
            Console.WriteLine($"[SaleCreatedEvent] Venda criada: {message.SaleId} - {message.SaleNumber}");
            await Task.CompletedTask;
        }
    }
}
