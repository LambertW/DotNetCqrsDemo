using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CQRSlite.Caching;
using CQRSlite.Commands;
using CQRSlite.Domain;
using CQRSlite.Events;
using CQRSlite.Messages;
using CQRSlite.Routing;
using DotNetCqrsDemo.Domain.CommandHandlers;
using DotNetCqrsDemo.Domain.EventStore;
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using DotNetCqrsDemo.Web.Commands.Filters;
using DotNetCqrsDemo.Web.Commands.Requests.Employees;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using StackExchange.Redis;

namespace DotNetCqrsDemo.Web.Commands
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();

            // Auto Mapper Extensions DI.
            services.AddAutoMapper();

            // Add cqrs services
            services.AddSingleton(new Router());
            services.AddSingleton<ICommandSender>(y => y.GetService<Router>());
            services.AddSingleton<IEventPublisher>(y => y.GetService<Router>());
            services.AddSingleton<IHandlerRegistrar>(y => y.GetService<Router>());
            services.AddSingleton<IEventStore, InMemoryEventStore>();
            services.AddSingleton<ICache, MemoryCache>();
            services.AddScoped<IRepository>(y => new CacheRepository(new Repository(y.GetService<IEventStore>()), y.GetService<IEventStore>(), y.GetService<ICache>()));
            services.AddScoped<ISession, Session>();

            // Redis Setting
            //var redisConfig = Configuration.Get<RedisConfiguration>();
            var multiplexer = ConnectionMultiplexer.Connect("127.0.0.1");
            services.AddSingleton<IConnectionMultiplexer>(multiplexer);

            // Use Scrutor extensions to scan assemblies.
            services.Scan(scan =>
            {
                scan.FromAssemblyOf<ILocationRepository>()
                .AddClasses(classes => classes.Where(x =>
                {
                    var allInterfaces = x.GetInterfaces();
                    return allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IBaseRepository<>));
                }))
                .AsImplementedInterfaces()
                .WithTransientLifetime();

                scan.FromAssemblyOf<LocationCommandHandler>()
                .AddClasses(classes => classes.Where(x =>
                {
                    var allInterfaces = x.GetInterfaces();
                    return allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(IHandler<>)) ||
                            allInterfaces.Any(y => y.GetTypeInfo().IsGenericType && y.GetTypeInfo().GetGenericTypeDefinition() == typeof(ICancellableHandler<>));
                }))
                .AsSelf()
                .WithTransientLifetime();
            });

            // Add framework services.
            services.AddMvc(config =>
            {
                config.Filters.Add(new BadRequestActionFilter());
            })
            .AddFluentValidation(fv =>
                fv.RegisterValidatorsFromAssemblyContaining<CreateEmployeeRequestValidator>()
            );

            // Register routes.
            var serviceProvider = services.BuildServiceProvider();
            var registrar = new RouteRegistrar(serviceProvider);
            registrar.RegisterInAssemblyOf(typeof(LocationCommandHandler));

            return serviceProvider;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
