using InvoiceWorker.Events.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;


namespace InvoiceWorker.DataSerializer
{
    public class EventSerializer : IEventSerializer
    {
        private readonly ILogger<EventSerializer> _logger;
        public EventSerializer(ILogger<EventSerializer> logger)
        {
            _logger = logger;
        }
        public EventFeedDetails DeserializeEventDetails(string eventDetails)
        {
            try
            {
                var eventFeedDetails=   JsonConvert.DeserializeObject<EventFeedDetails>(eventDetails);
                _logger.LogInformation("JSON deserialized a message into a EventFeedDetails object");
                return eventFeedDetails;
            }
            catch(Exception ex)
            {
                _logger.LogError("Error deserializing Json:{message}",ex.Message);
                return null;
            }

        }
    }
}
