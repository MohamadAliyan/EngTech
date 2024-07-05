using EngTech.Application.Contract.Commands.Products;
using EngTech.Domain.Entities.Products;
using FluentValidation;

namespace EngTech.Application.Handlers.Validator;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    private readonly IProductRepository _productRepository;


    public UpdateProductCommandValidator(IProductRepository productRepository)
    {
        _productRepository = productRepository;

        RuleFor(x => x.Title).Must((o, product) => { return UniqueName(o.Title, o.Id); });
        RuleFor(v => v.Price).GreaterThanOrEqualTo(1).WithMessage("Price GreaterThan 1");
        RuleFor(v => v.Discount).GreaterThanOrEqualTo(0).LessThanOrEqualTo(100)
            .WithMessage("Discount Must GreaterThan 0 and LessThan 100");
    }

    private bool UniqueName(string title, int productid)
    {
        return !_productRepository.ExistTitleProduct(title, productid);
    }
}