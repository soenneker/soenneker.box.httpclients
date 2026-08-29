[![](https://img.shields.io/nuget/v/soenneker.box.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.box.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.box.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.box.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.box.httpclients/)

# Soenneker.Box.HttpClients

A .NET thread-safe singleton HttpClient for.

## Install

```bash
dotnet add package Soenneker.Box.HttpClients
```

## Quick start

```csharp
using Soenneker.Box.HttpClients.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddBoxOpenApiHttpClientAsSingleton();
```

Adds `BoxOpenApiHttpClient` as a singleton service.

## What you get

- `IBoxOpenApiHttpClient` — A .NET thread-safe singleton HttpClient for.
- `BoxOpenApiHttpClientRegistrar` — Registers the OpenAPI HttpClient wrapper for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `BoxOpenApiHttpClientRegistrar.AddBoxOpenApiHttpClientAsSingleton(services)` | Adds `BoxOpenApiHttpClient` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `BoxOpenApiHttpClientRegistrar.AddBoxOpenApiHttpClientAsScoped(services)` | Adds `BoxOpenApiHttpClient` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
