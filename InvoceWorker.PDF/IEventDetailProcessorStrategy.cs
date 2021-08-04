using InvoiceWorker.Events.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public interface IEventDetailProcessorStrategy
    {
        bool Execute(Item eventFeedDetails);
    }
}
