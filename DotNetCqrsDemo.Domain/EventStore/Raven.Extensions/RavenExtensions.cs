using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents.Session;
using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetCqrsDemo.Domain.EventStore.Raven.Extensions
{
    public static class RavenExtensions
    {
        public static IServiceCollection AddRaven(this IServiceCollection services)
        {
            services.AddSingleton<IDocumentStoreHolder, DocumentStoreHolder>();
            services.AddScoped(p => p.GetRequiredService<IDocumentStoreHolder>().Store.OpenSession());

            return services;
        }
    }
}
