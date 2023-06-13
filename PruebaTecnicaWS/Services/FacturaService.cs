using Newtonsoft.Json;
using PruebaTecnicaWS.Interfaces;
using PruebaTecnicaWS.Models;

namespace PruebaTecnicaWS.Services
{
    public class FacturaService : IFactura
    {
        private readonly IApiConsumer _apiConsumerService;
        private readonly IConfiguration _configuration;
        public double Precio { get; set; }
        public FacturaService(IApiConsumer apiConsumerService, IConfiguration configuration)
        {
            _apiConsumerService = apiConsumerService;
            _configuration = configuration;
        }
        public async Task<FacturaGeneral> GetFacturaGeneral(string userID)
        {
            ConnectionWS connectionWS = _configuration.GetSection("ConnectionWS").Get<ConnectionWS>();
            HttpResponseMessage response = await _apiConsumerService.GetResponse(HttpMethod.Get, $"Ej1?userID={userID}&companyID={connectionWS.CompanyID}&portalID={connectionWS.PortalID}&facturaID={connectionWS.FacturaID}");
            string jsonFactura = response.Content.ReadAsStringAsync().Result;
            FacturaGeneral facturaGeneral = JsonConvert.DeserializeObject<FacturaGeneral>(jsonFactura);

            return facturaGeneral;
        }

        public void UpdatePrice(double precio)
        {
            Precio += precio;
        }
    }
}