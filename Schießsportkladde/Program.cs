using DataAccessLayer.Repositories;
using DataAccessLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;

namespace Schießsportkladde
{
    internal static class Program
    {
        static void Main()
        {
            //Initializes the application configuration settings
            ApplicationConfiguration.Initialize();

            //Configures and registers services (Dependencies) for the application
            ServiceCollection services = ConfigureServices();

            //Provides the services with their Dependencies
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            //Instance of the starting Form of the Application
            var startForm = serviceProvider.GetRequiredService<KladdenForm>();
            //Runs the application
            Application.Run(startForm);
        }

        static ServiceCollection ConfigureServices()
        {
            //Collection of registered Services
            ServiceCollection services = new ServiceCollection();

            //Registers Contracts for the Repositories
            services.AddTransient<IKladdenRepository>(_ => new KladdenRepository());
            services.AddTransient<ISchützenRepository>(_ => new SchützenRepository());
            services.AddTransient<IVerbändeRepository>(_ => new VerbändeRepository());
            services.AddTransient<IVereineRepository>(_ => new VereineRepository());
            services.AddTransient<IWaffenRepository>(_ => new WaffenRepository());
            services.AddTransient<ISchießstandRepository>(_ => new SchießstandRepository());
            services.AddTransient<IAufsichtRepository>(_ => new AufsichtRepository());
            services.AddTransient<IWettbewerbsRepository>(_ => new WettbewerbRepository());




            //Registers Forms for Dependency Injection
            services.AddTransient<KladdenForm>();
            services.AddTransient<SchützenForm>();            
            services.AddTransient<VereineForm>();
            services.AddTransient<VerbändeForm>();
            services.AddTransient<AufsichtForm>();
            services.AddTransient<SchießstandForm>();
            services.AddTransient<WaffenForm>();
            services.AddTransient<WettbewerbeForm>();
            services.AddTransient<StatistikenForm>();


            //Returns the configured Services
            return services;

        }
    }
}