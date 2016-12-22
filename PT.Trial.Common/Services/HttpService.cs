using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PT.Trial.Common.Contracts;

namespace PT.Trial.Common.Services
{
    public class HttpService : IHttpService
    {
        private string _headerName = "pt-calculation-id";

        public AppSettings Settings { get; }

        public HttpService(AppSettings settings)
        {
            Settings = settings;
        }

        public async Task<bool> SendAsync(Number number, string calculationId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Settings.WebConnectionString);

                var request = CreateJsonRequest(number, calculationId, HttpMethod.Post, "/api/numbers/");

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        private HttpRequestMessage CreateJsonRequest(object body, string calculationId, HttpMethod method, string resource)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, resource);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add(_headerName, calculationId);

            if (body != null)
                request.Content = new ObjectContent(body.GetType(), body, new JsonMediaTypeFormatter(), new MediaTypeHeaderValue("application/json"));

            return request;
        }

        public string ReadCalculationId(HttpRequestHeaders headers)
        {
            string value = null;

            IEnumerable<string> headerValues;

            if (headers.TryGetValues(_headerName, out headerValues))
            {
                value = headerValues.FirstOrDefault();
            }

            return value;
        }
    }
}