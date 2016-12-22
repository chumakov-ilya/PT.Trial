using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PT.Trial.Common
{
    public static class HttpService
    {
        public static async Task<bool> SendAsync(Number number, string calculationId)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:42424");

                var request = CreateJsonRequest(number, calculationId, HttpMethod.Post, "/api/numbers/");

                var response = await client.SendAsync(request);

                return response.IsSuccessStatusCode;
            }
        }

        private static HttpRequestMessage CreateJsonRequest(object body, string calculationId, HttpMethod method, string resource)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, resource);

            request.Headers.Accept.Clear();
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Add("pt-calculation-id", calculationId);

            if (body != null)
                request.Content = new ObjectContent(body.GetType(), body, new JsonMediaTypeFormatter(), new MediaTypeHeaderValue("application/json"));

            return request;
        }
    }
}