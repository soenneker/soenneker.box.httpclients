using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Box.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Box.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class BoxOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="IBoxOpenApiHttpClient"/> as a singleton service.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBoxOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IBoxOpenApiHttpClient, BoxOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IBoxOpenApiHttpClient"/> as a scoped service. Each scope owns a distinct named client.
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddBoxOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IBoxOpenApiHttpClient, BoxOpenApiHttpClient>();

        return services;
    }
}
