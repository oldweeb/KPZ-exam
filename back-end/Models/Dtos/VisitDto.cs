using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace back_end.Models.Dtos
{
    public class VisitDto
    {
        [JsonPropertyName("animalId")]
        public Guid AnimalId { get; set; }
        [JsonPropertyName("dateOfVisit")]
        public DateTime DateOfVisit { get; set; }
        [JsonPropertyName("diagnosis")]
        public string Diagnosis { get; set; }
    }
}
