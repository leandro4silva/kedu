using System.Text.Json.Serialization;

namespace Kedu.Application.Dtos
{
    public class CostCenterDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        public CostCenterDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
