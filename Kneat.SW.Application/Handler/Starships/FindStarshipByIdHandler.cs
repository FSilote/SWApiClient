using AutoMapper;
using Kneat.SW.Application.Command.Starships;
using Kneat.SW.Domain.Entity;
using Kneat.SW.Domain.Infrastructure.Gateways;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kneat.SW.Application.Handler.Starships
{
    public class FindStarshipByIdHandler : IRequestHandler<FindStarshipByIdCommand, Starship>
    {
        public FindStarshipByIdHandler(ISWApiGateway swApiGateway, IMapper mapper)
        {
            _swApiGateway = swApiGateway;
            _mapper = mapper;
        }

        private ISWApiGateway _swApiGateway;
        private IMapper _mapper;

        public Task<Starship> Handle(FindStarshipByIdCommand request, CancellationToken cancellationToken)
        {
            return Task.Run<Starship>(() =>
            {
                var apiModel = _swApiGateway.FindStarshipById(request.Id);
                return _mapper.Map<Starship>(apiModel);
            });
        }
    }
}
