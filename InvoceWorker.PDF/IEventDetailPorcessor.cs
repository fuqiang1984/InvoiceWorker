using InvoiceWorker.Events.Models;
using System;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public interface IEventDetailPorcessor
    {
        EventType ItemType { get; }
        bool Execute(Item eventFeedItem);
    }
}
