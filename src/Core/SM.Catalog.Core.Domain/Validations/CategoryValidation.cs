using FluentValidation;
using SM.Catalog.Core.Domain.Entities;

namespace SM.Catalog.Core.Domain.Validations
{
    public class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .WithMessage("O id da categria não foi informado.");

            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("A descrição da categoria não foi informada.");
        }
    }
}
