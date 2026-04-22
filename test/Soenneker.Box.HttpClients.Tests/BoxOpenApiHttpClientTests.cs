using Soenneker.Box.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Box.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class BoxOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IBoxOpenApiHttpClient _httpclient;

    public BoxOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IBoxOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
