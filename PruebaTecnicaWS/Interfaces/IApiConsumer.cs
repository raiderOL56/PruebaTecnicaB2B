using System.Net;
namespace PruebaTecnicaWS.Interfaces
{
    public interface IApiConsumer
    {
        Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint);
        Task<HttpResponseMessage> GetResponse(HttpMethod method, string endpoint, string json, string mimeType);
    }
}