using InvoceWorker.EventDetailPorcessor;
using InvoiceWorker.Events.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace InvoiceWorker.UnitTests.EventDetailProcessors
{
    public class EventDetailProcessorStrategyTest
    {
        private readonly List<IEventDetailPorcessor> _eventDetailPorcessors;
        private readonly Mock<IEventDetailPorcessor> _InvoiceCreatedPorcessor;
        private readonly Mock<IEventDetailPorcessor> _InvoiceUpdatedPorcessor;
        private readonly Mock<IEventDetailPorcessor> _InvoiceDeletedPorcessor;


        private readonly IEventDetailProcessorStrategy eventDetailProcessorStrategy;
        public EventDetailProcessorStrategyTest()
        {
            _eventDetailPorcessors = new List<IEventDetailPorcessor>();
            _InvoiceCreatedPorcessor = new Mock<IEventDetailPorcessor>();
            _InvoiceCreatedPorcessor.SetupGet(i => i.ItemType).Returns(EventType.INVOICE_CREATED);
            _InvoiceUpdatedPorcessor = new Mock<IEventDetailPorcessor>();
            _InvoiceUpdatedPorcessor.SetupGet(i => i.ItemType).Returns(EventType.INVOICE_UPDATED);
            _InvoiceDeletedPorcessor = new Mock<IEventDetailPorcessor>();
            _InvoiceDeletedPorcessor.SetupGet(i => i.ItemType).Returns(EventType.INVOICE_DELETED);
            _eventDetailPorcessors.Add(_InvoiceCreatedPorcessor.Object);
            _eventDetailPorcessors.Add(_InvoiceUpdatedPorcessor.Object);
            _eventDetailPorcessors.Add(_InvoiceDeletedPorcessor.Object);

            eventDetailProcessorStrategy = new EventDetailProcessorStrategy(_eventDetailPorcessors);

        }

        [Fact]
        public void InvoiceUpdated_Should_Return_True()
        {
            _InvoiceUpdatedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(true);
            var result= eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type= EventType.INVOICE_UPDATED });
            Assert.True(result);
        }

        [Fact]
        public void InvoiceUpdated_Should_Return_False()
        {
            _InvoiceUpdatedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(false);
            var result = eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type = EventType.INVOICE_UPDATED });
            Assert.False(result);
        }

        [Fact]
        public void InvoiceCreated_Should_Return_True()
        {
            _InvoiceCreatedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(true);
            var result = eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type = EventType.INVOICE_CREATED });
            Assert.True(result);
        }

        [Fact]
        public void InvoiceCreated_Should_Return_False()
        {
            _InvoiceCreatedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(false);
            var result = eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type = EventType.INVOICE_CREATED });
            Assert.False(result);
        }

        [Fact]
        public void InvoiceDeleted_Should_Return_True()
        {
            _InvoiceDeletedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(true);
            var result = eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type = EventType.INVOICE_DELETED });
            Assert.True(result);
        }

        [Fact]
        public void InvoiceDeleted_Should_Return_False()
        {
            _InvoiceDeletedPorcessor.Setup(i => i.Execute(It.IsAny<Item>())).Returns(false);
            var result = eventDetailProcessorStrategy.Execute(new Events.Models.Item() { Type = EventType.INVOICE_DELETED });
            Assert.False(result);
        }
    }
}
