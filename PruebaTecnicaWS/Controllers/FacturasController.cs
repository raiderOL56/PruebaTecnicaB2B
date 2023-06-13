using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PruebaTecnicaWS.Interfaces;
using PruebaTecnicaWS.Models;

namespace PruebaTecnicaWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class FacturasController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IApiConsumer _apiConsumerService;
        private readonly IFactura _facturaService;
        public FacturasController(IConfiguration configuration, IApiConsumer apiConsumerService, IFactura facturaService)
        {
            _configuration = configuration;
            _apiConsumerService = apiConsumerService;
            _facturaService = facturaService;
        }
        [HttpGet]
        public async Task<IActionResult> UpdateFactura(string userID)
        {
            try
            {
                FacturaGeneral facturaGeneral = await _facturaService.GetFacturaGeneral(userID);
                ConnectionWS connectionWS = _configuration.GetSection("ConnectionWS").Get<ConnectionWS>();

                List<string> idPartidas = new List<string>();

                foreach (Partida partida in facturaGeneral.partidas)
                {
                    idPartidas.Add(partida.id);
                    _facturaService.UpdatePrice(partida.Precio);
                }

                if ((facturaGeneral.total >= (_facturaService.Precio - .10)) && (facturaGeneral.total <= (_facturaService.Precio + .10)))
                {
                    var jsonObj = new { userID = userID, companyID = connectionWS.CompanyID, portalID = connectionWS.PortalID, facturaID = connectionWS.FacturaID, notification = "La factura fue adendada correctamente" };
                    string json = JsonConvert.SerializeObject(jsonObj);
                    HttpResponseMessage response = await _apiConsumerService.GetResponse(HttpMethod.Put, "Ej1", json, "application/json");

                    return Ok(new { idPartidas, Price = $"El precio total de la factura es ${facturaGeneral.total} | El precio total de las partidas es ${_facturaService.Precio}", UpdateFactura = response.StatusCode == HttpStatusCode.OK ? true : false });
                }
                else
                {
                    return BadRequest("Los precios totales no coinciden");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}