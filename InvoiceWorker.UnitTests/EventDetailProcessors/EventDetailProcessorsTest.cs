using InvoceWorker.EventDetailPorcessor;
using InvoiceWorker.DocumentServices;
using InvoiceWorker.Events.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InvoiceWorker.UnitTests.EventDetailProcessors
{
    public class EventDetailProcessorsTest
    {
        private readonly Mock<IDocumentService> _documentServiceMock;
        private readonly IEventDetailPorcessor _invoiceCreatedProcessor;
        private readonly IEventDetailPorcessor _invoiceUpdatedProcessor;
        private readonly IEventDetailPorcessor _invoiceDeletedProcessor;
        public EventDetailProcessorsTest()
        {
            _documentServiceMock = new Mock<IDocumentService>();
            _invoiceCreatedProcessor = new InvoceCreatedProcessor(_documentServiceMock.Object);
            _invoiceUpdatedProcessor = new InvoceUpdatedProcessor(_documentServiceMock.Object);
            _invoiceDeletedProcessor = new InvocieDeletedProcessor(_documentServiceMock.Object);
        }

        [Fact]
        public void InvoiceCreated_Should_Return_True()
        {
            _documentServiceMock.Setup(d => d.CreateInvoice(DocumentType.PDF, It.IsAny<Item>())).Returns(true);
            var result = _invoiceCreatedProcessor.Execute(new Item());

            Assert.True(result);
        }

        [Fact]
        public void InvoiceUpdated_Should_Return_True()
        {
            _documentServiceMock.Setup(d => d.UpdateInvoice(DocumentType.PDF, It.IsAny<Item>())).Returns(true);
            var result = _invoiceUpdatedProcessor.Execute(new Item());

            Assert.True(result);
        }

        [Fact]
        public void InvoiceUpdated_Should_Return_False()
        {
            _documentServiceMock.Setup(d => d.UpdateInvoice(DocumentType.PDF, It.IsAny<Item>())).Returns(false);
            var result = _invoiceUpdatedProcessor.Execute(new Item());

            Assert.False(result);
        }

        [Fact]
        public void InvoiceDeleted_Should_Return_True()
        {
            _documentServiceMock.Setup(d => d.DeleteInvoice(DocumentType.PDF, It.IsAny<Item>())).Returns(true);
            var result = _invoiceDeletedProcessor.Execute(new Item());

            Assert.True(result);
        }

        [Fact]
        public void InvoiceDeleted_Should_Return_False()
        {
            _documentServiceMock.Setup(d => d.DeleteInvoice(DocumentType.PDF, It.IsAny<Item>())).Returns(false);
            var result = _invoiceDeletedProcessor.Execute(new Item());

            Assert.False(result);
        }
    }
}
