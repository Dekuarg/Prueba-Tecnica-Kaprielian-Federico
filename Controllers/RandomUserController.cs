using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prueba_Tecnica_Kaprielian.Controllers
{
    [ApiController]
    [Route("api/randomuser")]
    public class RandomUserController : ControllerBase
    {
        private readonly IRandomUserApi _servicio;
        private readonly ILogger<RandomUserController> _logger;
        public RandomUserController(IRandomUserApi servicio, ILogger<RandomUserController> logger)
        {
            _servicio = servicio;
            _logger = logger;
        }
        [HttpGet]
        public async Task<ActionResult> GetRandom()
        {
            _logger.LogInformation("Entrada al endpoint GetRandom");

            ActionResult respuesta = await _servicio.GetRandom();

            _logger.LogInformation($"Salida al endpoint GetRandom");

            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult> SaveRandom([FromBody][Required] RandomUser data) 
        {
            _logger.LogInformation($"Entrada al endpoint SaveRandom y los siguientes parametros: {JsonConvert.SerializeObject(data)}");

            ActionResult respuesta = await _servicio.SaveRandom(data);

            _logger.LogInformation($"Salida al endpoint SaveRandom");

            return respuesta;
        }
    }
}
