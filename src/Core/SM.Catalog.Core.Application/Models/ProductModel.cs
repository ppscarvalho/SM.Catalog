namespace SM.Catalog.Core.Application.Models
{
    public class ProductModel
    {
        public Guid CategoryId { get; private set; }
        public string? Nome { get; private set; }
        public string? Description { get; private set; }
        public decimal PurchaseValue { get; private set; }
        public decimal SaleValue { get; private set; }
        public decimal ProfitMargin { get; private set; }
        public int Stock { get; private set; }
        public bool Status { get; private set; }
        public CategoryModel? Category { get; private set; }
    }
}
