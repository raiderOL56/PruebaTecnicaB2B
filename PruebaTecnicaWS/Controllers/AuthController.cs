using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaWS.Interfaces;
using PruebaTecnicaWS.Models;

namespace PruebaTecnicaWS.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : ControllerBase
    {
        private readonly IApiConsumer _apiConsumerService;
        public AuthController(IApiConsumer apiConsumerService)
        {
            _apiConsumerService = apiConsumerService;
        }

        [HttpGet]
        public async Task<ActionResult> SignUp(string userID)
        {
            try
            {
                HttpResponseMessage response = await _apiConsumerService.GetResponse(HttpMethod.Get, $"crear_usuario?userID={userID}");
                return Ok(response.Content.ReadAsStringAsync().Result);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}