using InvoiceWorker.Events.Models;
using System;

namespace InvoiceWorker.DataSerializer
{
    public interface IEventSerializer
    {
        EventFeedDetails DeserializeEventDetails(string eventDetails);
    }
}
