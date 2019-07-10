using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace BlazingPizza.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddScoped<OrderState>();
            // Add auth services
            _ = services.AddAuthorizationCore();
            _ = services.AddScoped<AuthenticationStateProvider, ServerAuthenticationStateProvider>();

        }
        public void Configure(IComponentsApplicationBuilder app)
        {
            app.AddComponent<App>("app");
        }
    }
}
