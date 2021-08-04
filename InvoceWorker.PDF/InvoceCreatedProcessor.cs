using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoceWorker.EventDetailPorcessor
{
    public class InvoceCreatedProcessor : IEventDetailPorcessor
    {
        private readonly IDocumentService _documentService;
        public InvoceCreatedProcessor(IDocumentService documentService)
        {
            _documentService = documentService;
        }
        public EventType ItemType => EventType.INVOICE_CREATED;

        public bool Execute(Item eventFeedItem)
        {
            return _documentService.CreateInvoice(DocumentType.PDF, eventFeedItem);
            
        }

    }
}
