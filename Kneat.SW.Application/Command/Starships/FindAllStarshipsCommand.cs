using Kneat.SW.Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kneat.SW.Application.Command.Starships
{
    public class FindAllStarshipsCommand : IRequest<ICollection<Starship>>
    {
    }
}
