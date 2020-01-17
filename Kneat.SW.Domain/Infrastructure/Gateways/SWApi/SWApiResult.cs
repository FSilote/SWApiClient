using System.Collections.Generic;

namespace Kneat.SW.Domain.Infrastructure.Gateways.SWApi
{
    public class SWApiResult<T>
    {
        public int Count { get; set; }
	    public string Next { get; set; }
	    public string Previous { get; set; }

        public ICollection<T> Results { get; set; } = new HashSet<T>();
    }
}
