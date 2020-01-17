using AutoMapper;
using Kneat.SW.Application.Command.Starships;
using Kneat.SW.Domain.Entity;
using Kneat.SW.Domain.Infrastructure.Gateways;
using Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Kneat.SW.Application.Handler.Starships
{
    public class FindAllStarshipsHandler : IRequestHandler<FindAllStarshipsCommand, ICollection<Starship>>
    {
        public FindAllStarshipsHandler(ISWApiGateway swApiGateway, IMapper mapper)
        {
            _swApiGateway = swApiGateway;
            _mapper = mapper;
        }

        private ISWApiGateway _swApiGateway;
        private IMapper _mapper;

        public Task<ICollection<Starship>> Handle(FindAllStarshipsCommand request, CancellationToken cancellationToken)
        {
            return Task.Run<ICollection<Starship>>(() =>
            {
                var starships = _swApiGateway.FindAllStarships();
                return starships.Select(s => _mapper.Map<StarshipApiModel, Starship>(s)).ToList();
            });
        }
    }
}
