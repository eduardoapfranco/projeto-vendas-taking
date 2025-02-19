using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Application.Sales.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IBus _bus;

        public CancelSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork, IBus bus)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _bus = bus;
        }

        public async Task<bool> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
            {
                return false;
            }

            sale.CancelSale();
            await _unitOfWork.CommitAsync();
            await _bus.Publish(new SaleCancelledEvent(sale.Id, sale.SaleNumber));
            return true;
        }
    }
}
