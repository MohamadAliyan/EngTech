using EngTech.Application.Contract.Commands.Products;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Product.Commands;

using static Testing;

public class CreateProductTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateProduct()
    {
        CreateProductCommand command = new() { Title = "کالا 1", Price = 10000, Discount = 5 };

        int res = await SendAsync(command);
        res.Should().BePositive();
    }
}
