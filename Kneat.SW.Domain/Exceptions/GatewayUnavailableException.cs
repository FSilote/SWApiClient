using System;

namespace Kneat.SW.Domain.Exceptions
{
    public class GatewayUnavailableException : Exception
    {
        public GatewayUnavailableException(string message) : base(message) { }
    }
}
