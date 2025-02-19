using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class UpdateSaleCommandHandler : IRequestHandler<UpdateSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.SaleId);
            if (sale == null)
            {
                return false;
            }

            sale.UpdateSale(request.SaleId ,request.SaleNumber, request.SaleDate, request.BranchId, request.BranchName, request.CustomerId, request.CustomerName);

            await _saleRepository.UpdateAsync(sale);
            await _unitOfWork.CommitAsync();
            return true;
        }
    }
}
