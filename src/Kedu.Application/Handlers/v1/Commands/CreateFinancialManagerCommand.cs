using System.Text.Json.Serialization;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class CreateFinancialManagerCommand : IRequest<CreateFinancialManagerResponse>
    {
        [JsonPropertyName("nome")]
        public string? Name { get; set; }
    }

    public class CreateFinancialManagerResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string? Name { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        public CreateFinancialManagerResponse(int id, string name, DateTime createDate)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
        }
    }
}
