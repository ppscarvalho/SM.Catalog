#nullable disable

using SM.Catalog.Core.Domain.Validations;
using SM.Resource.Domain;
using SM.Resource.Interfaces;

namespace SM.Catalog.Core.Domain.Entities
{
    public class Category : Entity, IAggregateRoot
    {
        public string Description { get; set; }

        // EF Relation
        public ICollection<Product> Product { get; set; }

        public Category() { }

        public Category(Guid id, string description)
        {
            Id = id;
            Description = description;
            IsValid();
        }

        public override bool IsValid()
        {
            ValidationResult = new CategoryValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
