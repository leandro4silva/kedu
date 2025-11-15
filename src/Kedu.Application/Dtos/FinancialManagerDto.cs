using System.Text.Json.Serialization;

namespace Kedu.Application.Dtos
{
    public class FinancialManagerDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        public FinancialManagerDto(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
