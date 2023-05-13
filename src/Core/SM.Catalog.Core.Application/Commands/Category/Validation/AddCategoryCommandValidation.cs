using FluentValidation;

namespace SM.Catalog.Core.Application.Commands.Category.Validation
{
    public class AddCategoryCommandValidation : AbstractValidator<AddCategoryCommand>
    {
        public AddCategoryCommandValidation()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informado");
        }
    }
}
