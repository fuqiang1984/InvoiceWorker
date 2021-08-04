using System.Text.Json.Serialization;

namespace InvoiceWorker.Events.Models
{
    // Root myDeserializedClass = JsonSerializer.Deserialize<Root>(myJsonResponse);
    public class LineItem
    {
        [JsonPropertyName("lineItemId")]
        public string LineItemId { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitCost")]
        public double UnitCost { get; set; }

        [JsonPropertyName("lineItemTotalCost")]
        public double LineItemTotalCost { get; set; }
    }


}
