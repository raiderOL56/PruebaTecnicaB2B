using PruebaTecnicaWS.Interfaces;

namespace PruebaTecnicaWS.Services
{
    public class ApiConsumerService : IApiConsumer
    {
        private readonly string _baseURL;
        public ApiConsumerService(string baseURL)
        {
            _baseURL = baseURL;
        }
        public async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(method, string.Concat(_baseURL, endpoint));

                return await client.SendAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, string json, string mimeType)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(method, string.Concat(_baseURL, endpoint));
                StringContent content = new StringContent(json, null, mimeType);
                request.Content = content;
                
                return await client.SendAsync(request);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}