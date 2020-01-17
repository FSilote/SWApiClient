using Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model;
using System.Collections.Generic;

namespace Kneat.SW.Domain.Infrastructure.Gateways
{
    public interface ISWApiGateway
    {
        StarshipApiModel FindStarshipById(int id);
        StarshipApiModel FindStarship(string name);
        ICollection<StarshipApiModel> FindAllStarships();
    }
}
