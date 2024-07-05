using EngTech.Application.Contract.Queries.Products;
using FluentValidation;

namespace EngTech.Application.Handlers.Validator;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("ProductId is required.");
    }
}