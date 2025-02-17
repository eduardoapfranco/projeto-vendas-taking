using MediatR;
using System;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands
{
    public class CancelSaleCommand : IRequest<bool>
    {
        public string SaleId { get; set; }
    }
}
