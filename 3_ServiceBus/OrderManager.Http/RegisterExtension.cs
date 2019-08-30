using System;
using Microsoft.Extensions.DependencyInjection;

namespace OrderManager.Http
{
    public static class RegisterExtension
    {
        /// <summary>
        /// Registers the HttpClient interface <see cref="IOrderManagerClient"/>.
        /// </summary>
        /// <param name="services"></param>
        /// <param name="serviceHost"></param>
        public static void AddOrderManagerHttpClient(
            this IServiceCollection services,
            Uri serviceHost)
        {
            services.AddHttpClient<IOrderManagerClient, OrderManagerClient>(client => client.BaseAddress = serviceHost);
        }
    }
}
