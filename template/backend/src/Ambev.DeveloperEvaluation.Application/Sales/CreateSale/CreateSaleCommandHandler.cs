using MediatR;
using Microsoft.Extensions.Logging;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;


namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, Guid>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateSaleCommandHandler> _logger;

        public CreateSaleCommandHandler(ISaleRepository saleRepository,
                                        IUnitOfWork unitOfWork,
                                        ILogger<CreateSaleCommandHandler> logger)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Guid> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
        {
            // Cria a venda
            var sale = Sale.Create(
                request.SaleNumber,
                request.SaleDate,
                request.BranchId,
                request.BranchName,
                request.CustomerId,
                request.CustomerName
            );

            // Adiciona itens
            if (request.Items != null)
            {
                foreach (var itemDto in request.Items)
                {
                    sale.AddItem(itemDto.ProductId, itemDto.ProductName,
                                 itemDto.UnitPrice, itemDto.Quantity);
                }
            }

            // Persiste
            await _saleRepository.AddAsync(sale);
            await _unitOfWork.CommitAsync();

            _logger.LogInformation("Sale created with Id: {SaleId}", sale.Id);

            return sale.Id;
        }
    }
}
