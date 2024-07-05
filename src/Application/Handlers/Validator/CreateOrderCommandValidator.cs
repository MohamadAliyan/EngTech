using EngTech.Application.Contract.Commands.Orders;
using EngTech.Application.Contract.Queries.Products;
using FluentValidation;

namespace EngTech.Application.Handlers.Validator;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.Count).GreaterThanOrEqualTo(1).WithMessage("  Product count must be greater than 1.");
 
    }
}