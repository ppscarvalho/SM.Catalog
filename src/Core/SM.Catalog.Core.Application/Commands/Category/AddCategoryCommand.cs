using SM.Catalog.Core.Application.Commands.Category.Validation;
using SM.Resource.Messagens;

namespace SM.Catalog.Core.Application.Commands.Category
{
    public class AddCategoryCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public string? Description { get; private set; }

        public AddCategoryCommand() { }

        public AddCategoryCommand(Guid id, string? description)
        {
            Id = id;
            Description = description;
        }

        public override bool IsValid()
        {
            ValidationResult = new AddCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
