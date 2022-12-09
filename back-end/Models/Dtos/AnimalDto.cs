using System.Text.Json.Serialization;

namespace back_end.Models.Dtos
{
    public class AnimalDto
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }
        [JsonPropertyName("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }
        [JsonPropertyName("owner")]
        public string OwnerName { get; set; }
        [JsonPropertyName("animalType")]
        public string Type { get; set; }
    }
}
