using MediatR;
using System.Text.Json.Serialization;

namespace Kedu.Application.Handlers.v1.Queries
{
    public class GetAllCostCentersCommand : IRequest<GetAllCostCentersResponse>
    {
        
    }

    public class GetAllCostCentersResponse
    {
        [JsonPropertyName("centrosDeCusto")]
        public List<CostCenterDto> CostCenters { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        public GetAllCostCentersResponse(List<CostCenterDto> costCenters, int total)
        {
            CostCenters = costCenters;
            Total = total;
        }
    }

    public class CostCenterDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("nome")]
        public string Name { get; set; }

        [JsonPropertyName("dataCriacao")]
        public DateTime CreateDate { get; set; }

        [JsonPropertyName("dataModificacao")]
        public DateTime? ModifyDate { get; set; }

        public CostCenterDto(int id, string name, DateTime createDate, DateTime? modifyDate)
        {
            Id = id;
            Name = name;
            CreateDate = createDate;
            ModifyDate = modifyDate;
        }
    }
}