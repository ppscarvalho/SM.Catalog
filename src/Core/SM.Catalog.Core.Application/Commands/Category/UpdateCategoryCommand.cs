#nullable disable

using FluentValidation;
using SM.Resource.Messagens;

namespace SM.Catalog.Core.Application.Commands.Category
{
    public class UpdateCategoryCommand : CommandHandler
    {
        public Guid Id { get; private set; }
        public string Description { get; private set; }

        public UpdateCategoryCommand() { }

        public UpdateCategoryCommand(Guid id, string description)
        {
            Id = id;
            Description = description;
        }
        public override bool IsValid()
        {
            ValidationResult = new UpdateCategoryCommandValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class UpdateCategoryCommandValidation : AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryCommandValidation()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informado");
        }
    }
}
