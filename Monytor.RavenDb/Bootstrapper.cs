﻿using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Monytor.Core.Repositories;
using Monytor.RavenDb.Indices;
using Monytor.RavenDb.Repositories;
using Raven.Client;
using Raven.Client.Document;

namespace Monytor.RavenDb {
    public static class Bootstrapper {
        public static void SetupDatabaseAndRegisterRepositories(ContainerBuilder containerBuilder,string connectionString) {
            var documentStore = SetupStore(connectionString);
            containerBuilder.RegisterInstance(documentStore).As<IDocumentStore>();
            containerBuilder.RegisterType<SeriesQueryRepository>().As<ISeriesQueryRepository>();
            containerBuilder.RegisterType<BulkRepository>().As<IBulkRepository>();
            containerBuilder.RegisterType<CollectorConfigQueryRepository>().As<ICollectorConfigQueryRepository>();
        }

        public static void SetupDatabaseAndRegisterRepositories(IServiceCollection serviceCollection, string databaseUrl, string databaseName) {
            var documentStore = SetupStore(databaseUrl, databaseName);
            serviceCollection.AddSingleton<IDocumentStore>(documentStore);
            serviceCollection.AddScoped<ISeriesRepository, SeriesRepository>();
            serviceCollection.AddScoped<ISeriesQueryRepository, SeriesQueryRepository>();
            serviceCollection.AddScoped<IDashboardRepository, DashboardRepository>();
            serviceCollection.AddScoped<ICollectorConfigRepository, CollectorConfigRepository>();
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        }

        private static DocumentStore SetupStore(string connectionString) {
            var documentStore = RavenHelper.CreateStore(connectionString);

            SetupIndexes(documentStore);
            return documentStore;
        }

        private static DocumentStore SetupStore(string databaseUrl, string databaseName) {
            var documentStore = RavenHelper.CreateStore(databaseUrl, databaseName);

            SetupIndexes(documentStore);
            return documentStore;
        }

        private static void SetupIndexes(DocumentStore documentStore) {
            new CollectorConfigIndex().SideBySideExecute(documentStore);
            new SeriesIndex().SideBySideExecute(documentStore);
            new SeriesByDayIndex().SideBySideExecute(documentStore);
            new SeriesByHourIndex().SideBySideExecute(documentStore);
            new TagGroupMapReduceIndex().SideBySideExecute(documentStore);
        }
    }
}
