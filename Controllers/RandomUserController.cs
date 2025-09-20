using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult> GetRandom() => await _servicio.GetRandom();

        [HttpPost]
        public async Task<ActionResult> SaveRandom([FromBody] RandomUser data) => await _servicio.SaveRandom(data);

    }
}
