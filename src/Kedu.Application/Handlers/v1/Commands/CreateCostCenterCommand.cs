using System.Text.Json.Serialization;
using MediatR;

namespace Kedu.Application.Handlers.v1.Commands
{
    public class CreateCostCenterCommand : IRequest<CreateCostCenterResponse>
    {
        [JsonPropertyName("nome")]
        public string Name { get; set; }
    }

    public class CreateCostCenterResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        public CreateCostCenterResponse(int id, string name, DateTime createDate)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
        }
    }
}