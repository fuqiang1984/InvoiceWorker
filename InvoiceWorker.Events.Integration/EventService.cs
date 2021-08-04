using InvoiceWorker.DataSerializer;
using InvoiceWorker.Events.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceWorker.Events.Integration
{
    public class EventService : IEventService
    {
        private readonly ILogger<EventService> _logger;
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IEventSerializer _eventSerializer;
        public EventService(IConfiguration configuration, IHttpClientFactory httpClientFactory, IEventSerializer eventSerializer, ILogger<EventService> logger)
        {
            _configuration = configuration;
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _eventSerializer = eventSerializer;
        }
        public async Task<EventFeedDetails> GetEventFeedDetails(int afterEventId)
        {
#if DEBUG
            string jsonContent = "{\"items\":[{\"id\":1,\"type\":\"INVOICE_CREATED\",\"content\":{\"invoiceId\":\"97f0821d-3517-471a-95f2-f00da84ec56e\",\"invoiceNumber\":\"INV-001\",\"lineItems\":[{\"lineItemId\":\"2686350b-2656-48a0-912d-763c06ef5c04\",\"description\":\"Supplies\",\"quantity\":2,\"unitCost\":10.15,\"lineItemTotalCost\":20.3}],\"status\":\"DRAFT\",\"dueDateUtc\":\"2020-04-30T10:00:00.000Z\",\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\",\"updatedDateUtc\":\"2020-04-19T10:00:00.000Z\"},\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\"}]}";
            return _eventSerializer.DeserializeEventDetails(jsonContent);
#else
            var eventFeedUrl = _configuration.GetValue<string>("EventFeedUrl");
            var pageSize = _configuration.GetValue<int>("DefaultPageSize");
            if (string.IsNullOrEmpty(eventFeedUrl))
            {
                throw new ConfigurationErrorsException(nameof(eventFeedUrl));
            }
            var httpClient = _httpClientFactory.CreateClient(HttpClients.EventFeedClient);
            var response =  httpClient.GetAsync($"{eventFeedUrl}/pageSize={pageSize}&afterEventId={afterEventId}").Result;
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            var jsonContent = await response.Content.ReadAsStringAsync();
            return  _eventSerializer.DeserializeEventDetails(jsonContent);
#endif

        }
    }
}
