using Kneat.SW.Domain.Exceptions;
using Kneat.SW.Domain.Infrastructure.Common;
using Kneat.SW.Domain.Infrastructure.Gateways;
using Kneat.SW.Domain.Infrastructure.Gateways.SWApi;
using Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Kneat.SW.Infrastructure.Gateways.SWApi_co
{
    public class SWApiGateway : ISWApiGateway
    {
        #region Ctrs

        public SWApiGateway(IConfiguration configuration, IHttpGateway httpGateway)
        {
            _configuration = configuration;
            _httpGateway = httpGateway;
        }

        #endregion

        #region Attrs

        private IConfiguration _configuration;
        private IHttpGateway _httpGateway;

        #endregion

        #region ISWApiGateway

        public StarshipApiModel FindStarshipById(int id)
        {
            var apiConfig = GetApiConfig();
            var uri = new Uri($"{apiConfig.host}{apiConfig.starshipsUrl}{id}");
            var httpResult = _httpGateway.Get<StarshipApiModel>(uri, null);

            if (!httpResult.Success)
                throw new GatewayUnavailableException($"Cannot connect to the SWApi.co: {httpResult?.Message ?? "No message available."}");

            return httpResult.Result;
        }

        public StarshipApiModel FindStarship(string name)
        {
            throw new NotImplementedException();
        }

        public ICollection<StarshipApiModel> FindAllStarships()
        {
            int page = 1, count = 0;
            var starships = new List<StarshipApiModel>();
            var apiConfig = GetApiConfig();

            do
            {
                var uri = new Uri($"{apiConfig.host}{apiConfig.starshipsUrl}?page={page}");
                var httpResult = _httpGateway.Get<SWApiResult<StarshipApiModel>>(uri, null);

                if (!httpResult.Success)
                    throw new GatewayUnavailableException($"Cannot connect to the SWApi.co: {httpResult?.Message ?? "No message available."}");

                count = httpResult.Result.Count;
                starships.AddRange(httpResult.Result.Results);

                page++;
            }
            while (starships.Count < count);

            return starships;
        }

        #endregion

        #region Private

        private (string host, string starshipsUrl, string filmsUrl, string planetsUrl, string vehiclesUrl, string speciesUrl) GetApiConfig()
        {
            var section = _configuration.GetSection("swApiGateway");

            var host = section.GetSection("host").Value;
            var starships = section.GetSection("starships").Value;
            var films = section.GetSection("films").Value;
            var planets = section.GetSection("planets").Value;
            var vehicles = section.GetSection("vehicles").Value;
            var species = section.GetSection("species").Value;

            return (host, starships, films, planets, vehicles, species);
        }

        #endregion
    }
}
