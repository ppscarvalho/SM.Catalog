#nullable disable


namespace SM.Catalog.Core.Domain.Entities
{
    public class Supplier
    {
        public Guid Id { get; set; }
        public string? CorporateName { get; set; }

        // EF Relation
        public ICollection<Product> Product { get; set; }

        public Supplier() { }
    }
}
