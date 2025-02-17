using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
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
            return true;
        }
    }
}
