using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty().WithMessage("O número da venda é obrigatório.");
            RuleFor(x => x.SaleDate).NotEmpty().WithMessage("A data da venda é obrigatória.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("O ID da filial é obrigatório.");
            RuleFor(x => x.BranchName).NotEmpty().WithMessage("O nome da filial é obrigatório.");
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("O ID do cliente é obrigatório.");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("O nome do cliente é obrigatório.");

            RuleForEach(x => x.Items).SetValidator(new SaleItemDtoValidator());
        }
    }

    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(x => x.ProductId).NotEmpty().WithMessage("O ID do produto é obrigatório.");
            RuleFor(x => x.ProductName).NotEmpty().WithMessage("O nome do produto é obrigatório.");
            RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("O preço unitário deve ser maior que zero.");
            RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
        }
    }
}
