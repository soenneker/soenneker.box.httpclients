using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Box.HttpClients.Abstract;

/// <summary>
/// Provides a configured, reusable HTTP client for the Box API.
/// </summary>
public interface IBoxOpenApiHttpClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the cached HTTP client for this utility instance.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel client initialization.</param>
    /// <returns>An HTTP client configured with the Box base address and authorization header.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
