[![](https://img.shields.io/nuget/v/soenneker.box.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.box.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.box.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.httpclients/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.box.httpclients/actions/workflows/codeql.yml)

# Soenneker.Box.HttpClients

Provides a reusable `HttpClient` configured for the Box API and dependency injection.

## Installation

```bash
dotnet add package Soenneker.Box.HttpClients
```

## Configuration

```json
{
  "Box": {
    "ApiKey": "<Box access token>",
    "ClientBaseUrl": "https://api.box.com/2.0",
    "AuthHeaderName": "Authorization",
    "AuthHeaderValueTemplate": "Bearer {token}"
  }
}
```

Only `Box:ApiKey` is required. The other values above show their defaults. `{token}` in `AuthHeaderValueTemplate` is replaced with the configured value.

Keep the access token in a secret provider rather than source control or a checked-in settings file.

## Registration

```csharp
using Microsoft.Extensions.DependencyInjection;
using Soenneker.Box.HttpClients.Registrars;

services.AddBoxOpenApiHttpClientAsSingleton();
```

`AddBoxOpenApiHttpClientAsScoped()` creates one wrapper and cached client per dependency-injection scope.

## Usage

```csharp
using Soenneker.Box.HttpClients.Abstract;

public sealed class BoxProfileClient
{
    private readonly IBoxOpenApiHttpClient _boxHttpClient;

    public BoxProfileClient(IBoxOpenApiHttpClient boxHttpClient)
    {
        _boxHttpClient = boxHttpClient;
    }

    public async ValueTask<string> GetCurrentUser(
        CancellationToken cancellationToken)
    {
        HttpClient client = await _boxHttpClient.Get(cancellationToken);

        using HttpResponseMessage response = await client.GetAsync(
            "/2.0/users/me",
            cancellationToken);

        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync(cancellationToken);
    }
}
```

For generated-client usage, pair this package with `Soenneker.Box.OpenApiClientUtil` rather than issuing HTTP requests directly.

## Lifecycle and behavior

- `Get` returns the same `HttpClient` for the lifetime of the registered wrapper.
- Do not dispose the returned client. Let the dependency-injection container dispose `IBoxOpenApiHttpClient` and its cache entry.
- Configuration is read when the client is first created. Later configuration changes do not rebuild that cached client.
- The cancellation token passed to `Get` applies to client initialization. Pass a token separately to every HTTP operation.
- The default base address is `https://api.box.com/2.0`. Direct `HttpClient` calls should use an appropriate Box API path; the example uses a root-relative `/2.0/...` path.
- Non-success responses are not converted to domain exceptions by this wrapper. Use `EnsureSuccessStatusCode`, inspect the response, or use the generated OpenAPI client.
