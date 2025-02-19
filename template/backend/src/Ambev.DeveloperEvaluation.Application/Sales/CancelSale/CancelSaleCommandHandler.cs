using MediatR;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Microsoft.Extensions.Logging;

namespace Ambev.DeveloperEvaluation.Application.Sales.Handlers
{
    public class CancelSaleCommandHandler : IRequestHandler<CancelSaleCommand, bool>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        public CancelSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork, ILogger<CreateSaleCommandHandler> logger)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
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
            _logger.LogInformation("Sale cancel with Id: {SaleId}", sale.Id);
            return true;
        }
    }
}
