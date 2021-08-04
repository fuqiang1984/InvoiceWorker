using InvoiceWorker.DataSerializer;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InvoiceWorker.UnitTests.DataSerializer
{
    public class EventSerializerTest
    {
        private readonly Mock<ILogger<EventSerializer>> _loggerMock;
        private readonly EventSerializer _eventSerializer;

        public EventSerializerTest()
        {
            _loggerMock = new Mock<ILogger<EventSerializer>>();
            _eventSerializer = new EventSerializer(_loggerMock.Object);
        }

        [Fact]
        public void DeserializeEventDetails_Should_Return_Valid_EventDetail()
        {
            string jsonContent = "{\"items\":[{\"id\":1,\"type\":\"INVOICE_CREATED\",\"content\":{\"invoiceId\":\"97f0821d-3517-471a-95f2-f00da84ec56e\",\"invoiceNumber\":\"INV-001\",\"lineItems\":[{\"lineItemId\":\"2686350b-2656-48a0-912d-763c06ef5c04\",\"description\":\"Supplies\",\"quantity\":2,\"unitCost\":10.15,\"lineItemTotalCost\":20.3}],\"status\":\"DRAFT\",\"dueDateUtc\":\"2020-04-30T10:00:00.000Z\",\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\",\"updatedDateUtc\":\"2020-04-19T10:00:00.000Z\"},\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\"}]}";

            var eventDetail =  _eventSerializer.DeserializeEventDetails(jsonContent);

            Assert.Equal(1, eventDetail.Items.Count);
            Assert.Equal(Events.Models.EventType.INVOICE_CREATED, eventDetail.Items[0].Type);
            Assert.Equal("97f0821d-3517-471a-95f2-f00da84ec56e", eventDetail.Items[0].Content.InvoiceId);

        }
    }
}
