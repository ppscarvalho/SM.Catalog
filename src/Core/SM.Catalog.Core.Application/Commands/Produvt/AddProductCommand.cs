using SM.Catalog.Core.Application.Commands.Produvt.Validation;
using SM.Resource.Messagens;

namespace SM.Catalog.Core.Application.Commands.Produvt
{
    public class AddProductCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public Guid CategoriaId { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal PurchaseValue { get; private set; }
        public decimal SaleValue { get; private set; }
        public decimal ProfitMargin { get; private set; }
        public int Stock { get; set; }

        public AddProductCommand(
            Guid id,
            Guid categoriaId,
            string? name,
            string? description,
            decimal purchaseValue,
            decimal saleValue,
            decimal profitMargin,
            int stock)
        {
            Id = id;
            CategoriaId = categoriaId;
            Name = name;
            Description = description;
            PurchaseValue = purchaseValue;
            SaleValue = saleValue;
            ProfitMargin = profitMargin;
            Stock = stock;

            IsValid();
        }

        public override bool IsValid()
        {
            ValidationResult = new AddProductCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
