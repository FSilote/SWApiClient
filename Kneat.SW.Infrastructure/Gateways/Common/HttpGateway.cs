using Kneat.SW.Domain.Infrastructure.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Kneat.SW.Domain.Infrastructure.Common.Model;

namespace Kneat.SW.Infrastructure.Gateways.Common
{
    public class HttpGateway : IHttpGateway
    {
        #region Ctrs

        public HttpGateway()
        {
        }

        #endregion

        #region Properties

        public JsonSerializerSettings JsonSettings => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        };

        #endregion

        #region IHttpGateway

        public HttpRequestResult<T> Post<T>(Uri uri, object model)
        {
            return DoRequest<T>(uri, model, "POST");
        }

        public HttpRequestResult<T> Put<T>(Uri uri, object model)
        {
            return DoRequest<T>(uri, model, "PUT");
        }

        public HttpRequestResult<T> Delete<T>(Uri uri)
        {
            return DoRequest<T>(uri, null, "DELETE");
        }

        public HttpRequestResult<T> Get<T>(Uri uri, IDictionary<string, string> queryParams)
        {
            if (queryParams != null && queryParams.Any())
            {
                var builder = new UriBuilder(uri);
                var query = HttpUtility.ParseQueryString(builder.Query);

                foreach (var p in queryParams)
                {
                    query.Add(p.Key, p.Value);
                }

                builder.Query = query.ToString();

                uri = new Uri(builder.ToString());
            }

            return DoRequest<T>(uri, null, "GET");
        }

        #endregion

        #region private

        private HttpRequestResult<T> DoRequest<T>(Uri uri, object model, string verb)
        {
            var result = new HttpRequestResult<T>();
            HttpResponseMessage httpResponseMessage = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = uri;
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var content = model != null
                    ? new StringContent(JsonConvert.SerializeObject(model, this.JsonSettings), Encoding.UTF8, "application/json")
                    : null;

                if ("POST".Equals(verb, StringComparison.InvariantCultureIgnoreCase))
                    httpResponseMessage = client.PostAsync(uri, content).Result;
                else if ("PUT".Equals(verb, StringComparison.InvariantCultureIgnoreCase))
                    httpResponseMessage = client.PutAsync(uri, content).Result;
                else if ("DELETE".Equals(verb, StringComparison.InvariantCultureIgnoreCase))
                    httpResponseMessage = client.DeleteAsync(uri).Result;
                else if ("GET".Equals(verb, StringComparison.InvariantCultureIgnoreCase))
                    httpResponseMessage = client.GetAsync(uri).Result;

                var resultContent = httpResponseMessage.Content.ReadAsStringAsync().Result;

                result.SetStatus(httpResponseMessage.IsSuccessStatusCode);
                result.SetMessage(httpResponseMessage.ReasonPhrase);

                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    T response = default(T);

                    if (!string.IsNullOrEmpty(resultContent))
                    {
                        response = JsonConvert.DeserializeObject<T>(resultContent);
                        result.SetResult(response);
                    }
                }
            }

            return result;
        }

        #endregion
    }
}
