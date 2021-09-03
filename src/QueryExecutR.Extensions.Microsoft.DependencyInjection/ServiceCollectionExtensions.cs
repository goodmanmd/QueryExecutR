using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using QueryExecutR.Extensions.Microsoft.DependencyInjection.Registration;

namespace QueryExecutR.Extensions.Microsoft.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddQueryExecutR(this IServiceCollection services, params Type[] markerTypes)
        {
            return services.AddQueryExecutR(markerTypes.Select(t => t.GetTypeInfo().Assembly));
        }

        public static IServiceCollection AddQueryExecutR(this IServiceCollection services, params Assembly[] assemblies)
        {
            return services.AddQueryExecutR(assemblies.AsEnumerable());
        }

        public static IServiceCollection AddQueryExecutR(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddRequiredServices();
            ServiceRegistrar.AddEQueryExecutRClasses(services, assemblies);

            return services;
        }

        private static void AddRequiredServices(this IServiceCollection services)
        {
            services.AddTransient<QueryHandlerFactory>(p => p.GetService);
            services.AddTransient<IQueryExecutor, QueryExecutor>();
        }
    }
}
