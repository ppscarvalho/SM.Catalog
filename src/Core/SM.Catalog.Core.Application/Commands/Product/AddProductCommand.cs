using SM.Catalog.Core.Application.Commands.Product.Validation;
using SM.Resource.Messagens;

namespace SM.Catalog.Core.Application.Commands.Product
{
    public class AddProductCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public Guid SupplierId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string? Name { get; private set; }
        public string? Description { get; private set; }
        public decimal PurchaseValue { get; private set; }
        public decimal SaleValue { get; private set; }
        public decimal ProfitMargin { get; private set; }
        public int Stock { get; private set; }

        public AddProductCommand() { }

        public AddProductCommand(
            Guid id,
            Guid supplierId,
            Guid categoryId,
            string? name,
            string? description,
            decimal purchaseValue,
            decimal saleValue,
            decimal profitMargin,
            int stock) : this()
        {
            Id = id;
            SupplierId = supplierId;
            CategoryId = categoryId;
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
