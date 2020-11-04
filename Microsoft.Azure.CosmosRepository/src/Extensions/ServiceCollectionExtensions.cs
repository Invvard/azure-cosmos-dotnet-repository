﻿// Copyright (c) IEvangelist. All rights reserved.
// Licensed under the MIT License.

using Microsoft.Azure.Cosmos;
using Microsoft.Azure.CosmosRepository;
using Microsoft.Azure.CosmosRepository.Options;
using Microsoft.Azure.CosmosRepository.Providers;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Extension methods for adding and configuring the Azure Cosmos DB services.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the services required to consume any number of <see cref="IRepository{TItem}"/> 
        /// instances to interact with Cosmos DB.
        /// </summary>
        /// <param name="services">The service collection to add services to.</param>
        /// <param name="setupAction">An action to configure the repository options</param>
        /// <returns>The same service collection that was provided, with the required cosmos services.</returns>
        public static IServiceCollection AddCosmosRepository(
            this IServiceCollection services,
            Action<RepositoryOptions> setupAction = default)
        {
            if (services is null)
            {
                throw new ArgumentNullException(
                    nameof(services), "A service collection is required.");
            }

            services.AddOptions<RepositoryOptions>()
                    .Configure<IConfiguration>((settings, configuration) =>
                                               {
                                                   configuration.GetSection(nameof(RepositoryOptions))
                                                                .Bind(settings);
                                               });
            services.AddLogging()
                    .AddHttpClient()
                    .AddSingleton<ICosmosClientOptionsProvider, DefaultCosmosClientOptionsProvider>()
                    .AddSingleton<ICosmosClientProvider, DefaultCosmosClientProvider>()
                    .AddSingleton(typeof(ICosmosContainerProvider<>), typeof(DefaultCosmosContainerProvider<>))
                    .AddSingleton<ICosmosPartitionKeyPathProvider, DefaultCosmosPartitionKeyPathProvider>()
                    .AddSingleton(typeof(IRepository<>), typeof(DefaultRepository<>))
                    .AddSingleton<IRepositoryFactory, DefaultRepositoryFactory>();

            if (setupAction != default)
            {
                services.PostConfigure(setupAction);
            }

            return services;
        }
    }
}