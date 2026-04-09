using Soenneker.Box.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Box.HttpClients.Tests;

[Collection("Collection")]
public sealed class BoxOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IBoxOpenApiHttpClient _httpclient;

    public BoxOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IBoxOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
