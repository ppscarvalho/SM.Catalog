namespace SM.Catalog.Core.Application.Models
{
    public class CategoryModel
    {
        public Guid? Id { get; set; }
        public string? Description { get; set; }
    }

    public class SupplierModel
    {
        public Guid? Id { get; set; }
        public string? CorporateName { get; set; }
    }
}
