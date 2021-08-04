using InvoiceWorker.Events.Models;
using Microsoft.Extensions.Configuration;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using System;
using System.IO;
using System.Text;

namespace InvoiceWorker.DocumentServices
{
    public class DocumentService: IDocumentService
    {

        private readonly IConfiguration _configuration;

        public DocumentService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool CreateInvoice(DocumentType documentType,Item eventFeedItem)
        {
            try
            {
                CreateOrUpdateInvoice(documentType, eventFeedItem, newFile: true);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            
            
        }

        private void CreateOrUpdateInvoice(DocumentType documentType, Item eventFeedItem,bool newFile= false)
        {
            switch (documentType)
            {
                case DocumentType.PDF:
                    PdfDocument document = CreatePDFLayOut(eventFeedItem);

#if DEBUG
                    var filePath = $"{eventFeedItem.Content.InvoiceId}.pdf";
#else
                    var invoiceDir = _configuration.GetValue<string>("InvoiceDir"); 
                    var filePath = $"{invoiceDir}\\{eventFeedItem.Content.InvoiceId}.pdf";
#endif
                    if (!File.Exists(filePath)&& newFile)
                    {
                        document.Save(filePath);
                    }
                    
                    if(!newFile)
                    {
                        document.Save(filePath);
                    }

                    break;
                default:
                    break;
            }
        }

        private static PdfDocument CreatePDFLayOut(Item eventFeedItem)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = $"Invoice {eventFeedItem.Content.InvoiceNumber}";

            // Create an empty page
            PdfPage page = document.AddPage();

            // Get an XGraphics object for drawing
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Create a font
            XFont font = new XFont("Verdana", 20, XFontStyle.BoldItalic);

            // Draw the text
            gfx.DrawString(eventFeedItem.Content.InvoiceNumber, font, XBrushes.Black,
              new XRect(0, 0, page.Width, page.Height),
              XStringFormats.Center);
            int Y = 20;
            foreach (var lineItem in eventFeedItem.Content.LineItems)
            {
                gfx.DrawString(lineItem.Description, font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
                Y += 20;
                gfx.DrawString($"Quantity:{lineItem.Quantity.ToString()}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
                Y += 20;
                gfx.DrawString($"${lineItem.UnitCost.ToString()}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
                Y += 20;
                gfx.DrawString($"${lineItem.LineItemTotalCost.ToString()}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
                Y += 20;

            }

            gfx.DrawString($"Status:{eventFeedItem.Content.Status.ToString()}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
            Y += 20;
            gfx.DrawString($"DueDate:{eventFeedItem.Content.DueDateUtc.ToString("yyyy-MM-dd")}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
            Y += 20;
            gfx.DrawString($"CreatedDate:{eventFeedItem.Content.CreatedDateUtc.ToString("yyyy-MM-dd")}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
            Y += 20;
            gfx.DrawString($"UpdatedDate:{eventFeedItem.Content.UpdatedDateUtc.ToString("yyyy-MM-dd")}", font, XBrushes.Black,
                 new XRect(0, Y, page.Width, page.Height),
                XStringFormats.Center);
            return document;
        }

        public bool UpdateInvoice(DocumentType documentType, Item eventFeedItem)
        {
            try
            {
                CreateOrUpdateInvoice(documentType, eventFeedItem, newFile: false);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public bool DeleteInvoice(DocumentType documentType, Item eventFeedItem)
        {
#if DEBUG
            var filePath = $"{eventFeedItem.Content.InvoiceId}.pdf";
#else
                    var invoiceDir = _configuration.GetValue<string>("InvoiceDir"); 
                    var filePath = $"{invoiceDir}\\{eventFeedItem.Content.InvoiceId}.pdf";
#endif
            try
            {
                DeleteFile(documentType, filePath);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
            

            return true;
        }

        private static void DeleteFile(DocumentType documentType, string filePath)
        {
            switch (documentType)
            {
                case DocumentType.PDF:
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
