using InvoiceWorker.Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public class EventDetailProcessorStrategy : IEventDetailProcessorStrategy
    {
        private readonly IEnumerable<IEventDetailPorcessor> _eventDetailPorcessors;
        public EventDetailProcessorStrategy(IEnumerable<IEventDetailPorcessor> eventDetailProcessorStrategy)
        {
            _eventDetailPorcessors = eventDetailProcessorStrategy;
        }
        public bool Execute(Item eventFeedItem)
        {
            if(eventFeedItem is null)
            {
                throw new ArgumentException(nameof(eventFeedItem));
            }

            return  _eventDetailPorcessors.FirstOrDefault(e => e.ItemType == eventFeedItem.Type)
                .Execute(eventFeedItem);
        }
    }
}
