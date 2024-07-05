using NUnit.Framework;

namespace Application.UnitTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetUp()
    {
        //await ResetState();
    }
}
