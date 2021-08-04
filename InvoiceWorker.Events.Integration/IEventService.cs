using InvoiceWorker.Events.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace InvoiceWorker.Events.Integration
{
    public interface IEventService
    {
        Task<EventFeedDetails> GetEventFeedDetails(int afterEventId);
    }
}
