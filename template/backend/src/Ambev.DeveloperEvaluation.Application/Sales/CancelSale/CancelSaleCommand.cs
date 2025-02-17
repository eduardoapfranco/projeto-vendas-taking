using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public Guid SaleId { get; set; }
    }
}
