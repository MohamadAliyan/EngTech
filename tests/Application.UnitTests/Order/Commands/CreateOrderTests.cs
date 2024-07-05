using EngTech.Application.Contract.Commands.Orders;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.Order.Commands;

public class CreateOrderTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateOrder()
    {
        CreateOrderCommand command = new() { Count = 1, ProductId = 1, UserId = 1 };

        int res = await Testing.SendAsync(command);
        res.Should().BePositive();
    }
}