using System.Text.Json.Serialization;

namespace back_end.Models.Dtos
{
    public class UpdateAnimalRequestBody
    {
        [JsonPropertyName("animalId")]
        public Guid AnimalId { get; set; }
        [JsonPropertyName("animal")]
        public AnimalDto Animal { get; set; }
    }
}
