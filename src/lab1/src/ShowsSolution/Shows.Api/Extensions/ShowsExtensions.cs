using Shows.Api.Models;

namespace Shows.Api.Extensions;

public static class ShowExtensions
{
    public static IServiceCollection AddShowServices(
        this IServiceCollection services)
    {
        services.AddScoped<ShowsCreateModelValidator>();
        return services;
    }
}
