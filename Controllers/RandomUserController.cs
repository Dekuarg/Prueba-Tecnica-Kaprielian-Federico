using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;

namespace Prueba_Tecnica_Kaprielian.Controllers
{
    [ApiController]
    [Route("api/randomuser")]
    public class RandomUserController : ControllerBase
    {
        private readonly IRandomUserApi _servicio;
        public RandomUserController(IRandomUserApi servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<ActionResult> GetRandom()
        {

            ActionResult respuesta = await _servicio.GetRandom();

            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult> SaveRandom([FromBody] RandomUser data) 
        {

            ActionResult respuesta = await _servicio.SaveRandom(data);

            return respuesta;
        }
    }
}
