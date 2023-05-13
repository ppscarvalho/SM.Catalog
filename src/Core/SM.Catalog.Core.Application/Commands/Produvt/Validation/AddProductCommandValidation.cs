using FluentValidation;

namespace SM.Catalog.Core.Application.Commands.Produvt.Validation
{
    public class AddProductCommandValidation : AbstractValidator<AddProductCommand>
    {
        public AddProductCommandValidation()
        {
            RuleFor(c => c.CategoriaId)
                .NotEqual(Guid.Empty)
                .WithMessage("Id da categoria inválido");

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição do produto não foi informada");

            RuleFor(c => c.PurchaseValue)
                .GreaterThan(0)
                .WithMessage("O valor de compra do produto precisa ser maior que 0 (zero).");

            RuleFor(c => c.SaleValue)
                .GreaterThan(0)
                .WithMessage("O valor de venda do produto precisa ser maior que 0 (zero).");
        }
    }
}
