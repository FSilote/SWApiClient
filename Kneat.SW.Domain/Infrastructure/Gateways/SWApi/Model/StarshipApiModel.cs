using Newtonsoft.Json;
using System.Collections.Generic;

namespace Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model
{
    public class StarshipApiModel : SWApiBaseModel
    {
        public string Name { get; set; }
        public string Model { get; set; }
        public string Consumables { get; set; }
        public string Manufacturer { get; set; }
        public string Length { get; set; }
        public string Crew { get; set; }
        public string Passengers { get; set; }

        [JsonProperty(PropertyName = "starship_class")]
        public string StarshipClass { get; set; }

        [JsonProperty(PropertyName = "cost_in_credits")]
        public string CostInCredits { get; set; }

        [JsonProperty(PropertyName = "max_atmosphering_speed")]
        public string MaxAtmospheringSpeed { get; set; }

        [JsonProperty(PropertyName = "hyperdrive_rating")]
        public string HyperdriveRating { get; set; }

        [JsonProperty(PropertyName = "MGLT")]
        public string Mglt { get; set; }

        [JsonProperty(PropertyName = "cargo_capacity")]
        public string CargoCapacity { get; set; }

        public ICollection<string> Films { get; set; } = new LinkedList<string>();

        public ICollection<string> Pilots { get; set; } = new LinkedList<string>();
    }
}
