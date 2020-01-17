using System;

namespace Kneat.SW.Domain.Infrastructure.Gateways.SWApi.Model
{
    public abstract class SWApiBaseModel
    {
        public string Url { get; set; }
        public DateTime Created { get; set; }
        public DateTime Edited { get; set; }
    }
}
