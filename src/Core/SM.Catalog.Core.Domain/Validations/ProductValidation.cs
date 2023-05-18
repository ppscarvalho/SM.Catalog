using FluentValidation;
using SM.Catalog.Core.Domain.Entities;

namespace SM.Catalog.Core.Domain.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(c => c.Id)
                .NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido.");

            RuleFor(c => c.SupplierId)
                .NotEqual(Guid.Empty)
                .WithMessage("Fornecedor do produto não informado.");

            RuleFor(c => c.CategoryId)
                .NotEqual(Guid.Empty)
                .WithMessage("Categoria do produto não informado.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição do produto não foi informada.");

            RuleFor(c => c.PurchaseValue)
                .GreaterThan(0)
                .WithMessage("O valor de compra do produto precisa ser maior que 0 (zero).");

            RuleFor(c => c.ProfitMargin)
                .GreaterThan(0)
                .WithMessage("O valor de venda do produto precisa ser maior que 0 (zero).");
        }
    }
}
