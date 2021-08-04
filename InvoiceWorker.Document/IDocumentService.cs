using InvoiceWorker.Events.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceWorker.DocumentServices
{
    public interface IDocumentService
    {
        bool CreateInvoice(DocumentType documentType, Item eventFeedItem);
        bool UpdateInvoice(DocumentType documentType, Item eventFeedItem);

        bool DeleteInvoice(DocumentType documentType, Item eventFeedItem);
    }
}
