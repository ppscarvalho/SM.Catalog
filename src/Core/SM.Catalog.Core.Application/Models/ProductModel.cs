namespace SM.Catalog.Core.Application.Models
{
    public class ProductModel
    {
        public Guid? Id { get; set; }
        public Guid SupplierId { get; set; }
        public Guid CategoryId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal PurchaseValue { get; set; }
        public decimal SaleValue { get; set; }
        public decimal ProfitMargin { get; set; }
        public int Stock { get; set; }
    }
}
