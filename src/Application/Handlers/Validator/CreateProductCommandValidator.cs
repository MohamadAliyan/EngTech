using EngTech.Application.Contract.Commands.Products;
using EngTech.Domain.Entities.Products;
using FluentValidation;

namespace EngTech.Application.Handlers.Validator;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;


    public CreateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        // check validation before commad handler
        RuleFor(v => v.Title).NotEmpty().MaximumLength(40).Must(UniqueName)
            .WithMessage("Duplicate Product Title");

        RuleFor(v => v.Price)
            .GreaterThanOrEqualTo(1)
            .WithMessage("Price GreaterThan 1");

        RuleFor(v => v.Discount)
            .GreaterThanOrEqualTo(0)
            .LessThanOrEqualTo(100)
            .WithMessage("Discount Must GreaterThan 0 and LessThan 100");
    }

    private bool UniqueName(string title)
    {
        return !_productRepository.ExistTitleProduct(title);
    }
}


