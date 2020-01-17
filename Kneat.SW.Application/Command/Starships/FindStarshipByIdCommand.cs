using Kneat.SW.Domain.Entity;
using MediatR;

namespace Kneat.SW.Application.Command.Starships
{
    public class FindStarshipByIdCommand : IRequest<Starship>
    {
        public FindStarshipByIdCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
