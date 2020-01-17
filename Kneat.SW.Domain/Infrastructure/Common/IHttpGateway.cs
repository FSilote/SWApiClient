using Kneat.SW.Domain.Infrastructure.Common.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Kneat.SW.Domain.Infrastructure.Common
{
    public interface IHttpGateway
    {
        JsonSerializerSettings JsonSettings { get; }

        HttpRequestResult<T> Post<T>(Uri uri, object model);
        HttpRequestResult<T> Put<T>(Uri uri, object model);
        HttpRequestResult<T> Delete<T>(Uri uri);
        HttpRequestResult<T> Get<T>(Uri uri, IDictionary<string, string> queryParams);
    }
}
