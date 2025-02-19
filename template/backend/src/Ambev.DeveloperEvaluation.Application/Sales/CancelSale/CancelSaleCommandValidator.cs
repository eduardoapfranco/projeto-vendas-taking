using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators
{
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty().WithMessage("SaleId is required.");
        }
    }
}
