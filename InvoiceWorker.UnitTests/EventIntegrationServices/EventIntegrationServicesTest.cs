using InvoiceWorker.DataSerializer;
using InvoiceWorker.Events.Integration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace InvoiceWorker.UnitTests.EventIntegrationServices
{
    public class EventIntegrationServicesTest
    {
        private readonly Mock<ILogger<EventService>> _loggerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;
        private readonly Mock<IEventSerializer> _eventSerializerMock;
        private readonly IEventService _eventService;
        private readonly Mock<TestHttpMessageHandler> _httpMessageHandlerMock;

       

        public EventIntegrationServicesTest()
        {
            _configurationMock = new Mock<IConfiguration>();
            _httpMessageHandlerMock = new Mock<TestHttpMessageHandler> { CallBase = true };
             SetConfigurationSection("EventFeedUrl", "https://www.xeroapi.com/invoices/events");

            _loggerMock = new Mock<ILogger<EventService>>();
            _httpClientFactoryMock = new Mock<IHttpClientFactory>();
            _eventSerializerMock = new Mock<IEventSerializer>();
            _eventService = new EventService(_configurationMock.Object, _httpClientFactoryMock.Object, _eventSerializerMock.Object, _loggerMock.Object);
        }

        private void SetHttpClientFactoryToReturnClient()
        {
            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            _httpClientFactoryMock.Setup(m => m.CreateClient(HttpClients.EventFeedClient)).Returns(httpClient);
        }
        private void SetConfigurationSection(string key, string value)
        {
            var configurationSection = new Mock<IConfigurationSection>();
            _configurationMock.Setup(m => m.GetSection(key)).Returns(configurationSection.Object);
            configurationSection.Setup(m => m.Value).Returns(value);
        }

        private void SetHttpClientResponse(HttpStatusCode httpStatusCode, string content)
        {
            _httpMessageHandlerMock.Setup(f => f.Send(It.IsAny<HttpRequestMessage>())).Returns(
                new HttpResponseMessage()
                {
                    StatusCode = httpStatusCode,
                    Content = new StringContent(content)
                }
                );
        }

        private void SetEventSerializer()
        {
            _eventSerializerMock.Setup(e => e.DeserializeEventDetails(It.IsAny<string>())).Returns(
                 new Events.Models.EventFeedDetails()
                 {
                     Items = new List<Events.Models.Item>() {
                    new Events.Models.Item(){ Content=new Events.Models.Content(){ InvoiceId="MockId" } }
                   }
                 }
                );
            
        }

        [Fact]
        public async Task GetEventFeedDetails_Should_Return_Valid_EventDetails()
        {
            var jsonContent = "{\"items\":[{\"id\":1,\"type\":\"INVOICE_CREATED\",\"content\":{\"invoiceId\":\"97f0821d-3517-471a-95f2-f00da84ec56e\",\"invoiceNumber\":\"INV-001\",\"lineItems\":[{\"lineItemId\":\"2686350b-2656-48a0-912d-763c06ef5c04\",\"description\":\"Supplies\",\"quantity\":2,\"unitCost\":10.15,\"lineItemTotalCost\":20.3}],\"status\":\"DRAFT\",\"dueDateUtc\":\"2020-04-30T10:00:00.000Z\",\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\",\"updatedDateUtc\":\"2020-04-19T10:00:00.000Z\"},\"createdDateUtc\":\"2020-04-19T10:00:00.000Z\"}]}";

            SetHttpClientResponse(HttpStatusCode.OK, jsonContent);
            SetHttpClientFactoryToReturnClient();
            SetEventSerializer();

           var result= await _eventService.GetEventFeedDetails(5).ConfigureAwait(false);
            Assert.Equal("MockId", result.Items[0].Content.InvoiceId);
           
          
        }
    }
    public class TestHttpMessageHandler:HttpMessageHandler
    {
        public virtual HttpResponseMessage Send(HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
        protected override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(Send(request));
        }

       
    }
}
