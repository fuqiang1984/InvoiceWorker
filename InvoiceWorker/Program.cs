using InvoceWorker.EventDetailPorcessor;
using InvoiceWorker.DataSerializer;
using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Integration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceWorker
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient(HttpClients.EventFeedClient, client=> { client.Timeout = TimeSpan.FromSeconds(90); });
                    services.AddSingleton<IEventSerializer, EventSerializer>();
                    services.AddSingleton<IEventService, EventService>();
                    services.AddSingleton<IDocumentService, DocumentService>();
                    services.AddSingleton<IEventDetailProcessorStrategy, EventDetailProcessorStrategy>();
                    services.AddSingleton<IEventDetailPorcessor, InvoceCreatedProcessor>();
                    services.AddSingleton<IEventDetailPorcessor, InvoceUpdatedProcessor>();
                    services.AddSingleton<IEventDetailPorcessor, InvocieDeletedProcessor>();
                    services.AddHostedService<Worker>();
                });
    }
}
