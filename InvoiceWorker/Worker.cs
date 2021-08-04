using InvoceWorker.EventDetailPorcessor;
using InvoiceWorker.Events.Integration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private readonly IEventService _eventService;

        private readonly IEventDetailProcessorStrategy _eventDetailProcessorStrategy;

        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IEventService eventService, 
            IEventDetailProcessorStrategy eventDetailProcessorStrategy,IConfiguration configuration)
        {
            _logger = logger;
            _eventService = eventService;
            _eventDetailProcessorStrategy = eventDetailProcessorStrategy;
            _configuration = configuration;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            
            if(File.Exists("LatestEventID.txt") && int.TryParse(File.ReadAllText("LatestEventID.txt"),out int afterEventId))
            {
               
                while (!stoppingToken.IsCancellationRequested)
                {
                    _logger.LogInformation("Pooling the event feed {afterEventId}", afterEventId);
                    var eventFeedDetails = await _eventService.GetEventFeedDetails(afterEventId);
                    if (eventFeedDetails?.Items is null)
                    {
                        _logger.LogInformation("No event details captured!");
                    }
                    foreach (var item in eventFeedDetails.Items)
                    {
                         _eventDetailProcessorStrategy.Execute(item);
                        _logger.LogInformation("Event {id} has invoice {invoiceId} triggered action type {type}", afterEventId, item.Content.InvoiceNumber, item.Type);
                    }

                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    await Task.Delay(1000, stoppingToken);

                    afterEventId++;
                    File.WriteAllText("LatestEventID.txt", afterEventId.ToString());
                }
            }
            else
            {
                _logger.LogInformation("Please check appsettings.json for key afterEventId");
            }
           
        }


    }
}
