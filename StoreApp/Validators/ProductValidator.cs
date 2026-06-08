using FluentValidation;
using StoreApp.DTOs;

namespace StoreApp.Validators;

public class ProductValidator : AbstractValidator<CreateProductDto>
{
    public ProductValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Price)
            .GreaterThan(0);
    }
}