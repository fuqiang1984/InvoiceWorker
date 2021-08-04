using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Models;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;


namespace InvoiceWorker.UnitTests.DocumentServices
{
    public class DocumentServicesTest
    {
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly IDocumentService documentService;

        public DocumentServicesTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            SetConfigurationSection("InvoiceDir", "C:\\");
            documentService = new DocumentService(_configurationMock.Object);
        }

        private void SetConfigurationSection(string key, string value)
        {
            var configurationSection = new Mock<IConfigurationSection>();
            _configurationMock.Setup(m => m.GetSection(key)).Returns(configurationSection.Object);
            configurationSection.Setup(m => m.Value).Returns(value);
        }
        [Fact]
        public void CreateInvoice_Should_Return_True()
        {
            var eventFeedItem = new Item()
            {
                Id = 1234,
                CreatedDateUtc = DateTime.UtcNow,
                Content= new Content() { InvoiceId="mockId", InvoiceNumber="MockNumber", Status="Mockstatus", LineItems=new List<LineItem>() { new LineItem() {  Description="mockDesc", LineItemId="mockLineId", LineItemTotalCost=3.14, Quantity=2, UnitCost=1.15} } } , 
                Type= EventType.INVOICE_CREATED
                 
            };
            var result= documentService.CreateInvoice(DocumentType.PDF, eventFeedItem);
            Assert.True(result);
        }
    }
}
