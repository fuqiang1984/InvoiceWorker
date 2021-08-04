using System;
using System.Text.Json.Serialization;

namespace InvoiceWorker.Events.Models
{
    public class Item
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("type")]
        public EventType Type { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }

        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }
    }

    public enum EventType
    { 
        INVOICE_CREATED,
        INVOICE_UPDATED,
        INVOICE_DELETED
    }

}
