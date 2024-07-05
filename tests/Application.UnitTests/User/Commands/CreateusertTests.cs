using EngTech.Application.Contract.Commands.Users;
using FluentAssertions;
using NUnit.Framework;

namespace Application.UnitTests.User.Commands;

public class CreateusertTests : BaseTestFixture
{
    [Test]
    public async Task ShouldCreateUser()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateUserCommand command = new() { Name = $"{i} کاربر" };
            int res = await Testing.SendAsync(command);
            res.Should().BePositive();
        }
    }
}