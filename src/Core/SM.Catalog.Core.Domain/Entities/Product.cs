using SM.Catalog.Core.Domain.Validations;
using SM.Resource.Domain;
using SM.Resource.Interfaces;

namespace SM.Catalog.Core.Domain.Entities
{
    public class Product : Entity, IAggregateRoot
    {
        public Guid SupplierId { get; private set; }
        public Guid CategoryId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public decimal PurchaseValue { get; private set; }
        public decimal SaleValue { get; private set; }
        public decimal ProfitMargin { get; private set; }
        public int Stock { get; private set; }
        public bool Status { get; private set; }
        public Category Category { get; private set; }
        public Supplier Supplier { get; private set; }

        public Product() { }

        public Product(
            Guid supplierId,
            Guid categoryId,
            string nome,
            string description,
            decimal purchaseValue,
            decimal saleValue,
            decimal profitMargin)
        {
            SupplierId = supplierId;
            CategoryId = categoryId;
            Name = nome;
            Description = description;
            PurchaseValue = purchaseValue;
            SaleValue = saleValue;
            ProfitMargin = profitMargin;
        }

        public void Enbled() => Status = true;

        public void Disable() => Status = false;

        public void StockReplacement(int stock)
        {
            Stock += stock;
        }

        public override bool IsValid()
        {
            ValidationResult = new ProductValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
