using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Models;
using System;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public class InvoceUpdatedProcessor : IEventDetailPorcessor
    {
        public EventType ItemType => EventType.INVOICE_UPDATED;
        private readonly IDocumentService _documentService;
        public InvoceUpdatedProcessor(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public bool Execute(Item eventFeedItem)
        {

            return _documentService.UpdateInvoice( DocumentType.PDF,eventFeedItem);
        }
    }
}
