using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Planner.Infrastructure
{
    public static class IoCManager
    {

        public static void Register(this IServiceCollection service)
        {
            AddContext(service);
            AddRepositories(service);
            AddQueries(service);
            AddUseCases(service);
        }
        public static void AddConfig(this IServiceCollection service, IConfiguration config)
        {
            service.Configure<PlannerAppConfig>(config.GetSection(PlannerAppConfig.Configuration));
        }

        private static void AddQueries(IServiceCollection service)
        {
            const string suffix = "Queries";
            var types = typeof(IoCManager).Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract &&
                    type.Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase) &&
                    type.GetInterfaces().First().Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase)
                );

            foreach (var type in types)
            {
                service.AddScoped(type.GetInterfaces().First(), type);
            }
        }

        private static void AddUseCases(IServiceCollection service)
        {
            const string suffix = "UseCase";
            Assembly applicationAssembly = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Single(x => x.GetName().Name == "Planner.Application");

            IEnumerable<Type> types = applicationAssembly
                .GetTypes()
                .Where(type => !type.IsAbstract &&
                    type.Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase) &&
                    type.GetInterfaces().First().Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase)
                );

            foreach (var type in types)
            {
                service.AddScoped(type.GetInterfaces().First(), type);
            }
        }

        private static void AddRepositories(IServiceCollection service)
        {
            const string suffix = "Repository";
            var types = typeof(IoCManager).Assembly
                .GetTypes()
                .Where(type => !type.IsAbstract &&
                    type.Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase) &&
                    type.GetInterfaces().First().Name.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase)
                );

            foreach (var type in types)
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    service.AddScoped(interfaceType, type);
                }
            }
        }

        private static void AddContext(IServiceCollection service)
        {
            service.AddSingleton<Context>();
        }

    }
}
