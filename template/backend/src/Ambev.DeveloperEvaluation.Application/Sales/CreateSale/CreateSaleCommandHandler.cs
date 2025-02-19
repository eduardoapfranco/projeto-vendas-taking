using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Application.Sales.Events;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler : IRequestHandler<CreateSaleCommand, string>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBus _bus;

        public CreateSaleCommandHandler(ISaleRepository saleRepository, IUnitOfWork unitOfWork, IMapper mapper, IBus bus)
        {
            _saleRepository = saleRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _bus = bus;
        }


        public async Task<string> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
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

            await _bus.Publish(new SaleCreatedEvent(sale.Id, sale.SaleNumber));

            return sale.Id;
        }
    }
}
