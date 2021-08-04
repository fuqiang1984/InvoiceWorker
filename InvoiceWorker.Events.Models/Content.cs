using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace InvoiceWorker.Events.Models
{
    public class Content
    {
        [JsonPropertyName("invoiceId")]
        public string InvoiceId { get; set; }

        [JsonPropertyName("invoiceNumber")]
        public string InvoiceNumber { get; set; }

        [JsonPropertyName("lineItems")]
        public List<LineItem> LineItems { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("dueDateUtc")]
        public DateTime DueDateUtc { get; set; }

        [JsonPropertyName("createdDateUtc")]
        public DateTime CreatedDateUtc { get; set; }

        [JsonPropertyName("updatedDateUtc")]
        public DateTime UpdatedDateUtc { get; set; }
    }


}
