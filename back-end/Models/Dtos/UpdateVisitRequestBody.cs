using System.Text.Json.Serialization;

namespace back_end.Models.Dtos
{
    public class UpdateVisitRequestBody
    {
        [JsonPropertyName("visitId")]
        public Guid VisitId { get; set; }
        [JsonPropertyName("visit")]
        public VisitDto Visit { get; set; }
    }
}
