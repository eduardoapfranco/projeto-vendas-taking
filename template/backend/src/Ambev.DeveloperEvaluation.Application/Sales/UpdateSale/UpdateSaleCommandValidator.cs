using FluentValidation;
using Ambev.DeveloperEvaluation.Application.Sales.Commands;

namespace Ambev.DeveloperEvaluation.Application.Sales.Validators
{
    public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(x => x.SaleId).NotEmpty().WithMessage("SaleId is required.");
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("SaleNumber is required.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("SaleDate is required.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId is required.");
            RuleFor(x => x.BranchName).NotEmpty().WithMessage("BranchName is required.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId is required.");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("CustomerName is required.");
        }
    }
}
