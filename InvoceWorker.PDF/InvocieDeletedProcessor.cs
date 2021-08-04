using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Models;
using System;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public class InvocieDeletedProcessor : IEventDetailPorcessor
    {
        public EventType ItemType => EventType.INVOICE_DELETED;

        private readonly IDocumentService _documentService;
        public InvocieDeletedProcessor(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public bool Execute(Item eventFeedItem)
        {
           return _documentService.DeleteInvoice( DocumentType.PDF, eventFeedItem);

        }
    }
}
