using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InvoiceWorker.Events.Models
{

    public class EventFeedDetails
    {
        [JsonPropertyName("items")]
        public List<Item> Items { get; set; }
    }


}
