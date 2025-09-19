using Dominio.Modelos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Kaprielian.Controllers
{
    [ApiController]
    [Route("api/objects")]
    public class ObjectsController : ControllerBase
    {
        private readonly IApiObject _servicio;
        private readonly ILogger<ObjectsController> _logger;
        public ObjectsController(IApiObject servicio, ILogger<ObjectsController> logger)
        {
            _servicio = servicio;
            _logger = logger;
        }
        [HttpGet] 
        public async Task<ActionResult> GetAll() 
        {
            _logger.LogInformation("Entrada al endpoint GetAll");

            ActionResult respuesta = await _servicio.GetAll();

            _logger.LogInformation($"Salida al endpoint GetAll");

            return respuesta;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([Required] string id) 
        {
            _logger.LogInformation($"Entrada al endpoint GetById y el id: {id}");

            ActionResult respuesta = await _servicio.GetById(id);

            _logger.LogInformation($"Salida al endpoint GetById");

            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody][Required] ApiObjectCreateModel data) 
        {
            _logger.LogInformation($"Entrada al endpoint Create y los siguientes parametros: {JsonConvert.SerializeObject(data)}");

            ActionResult respuesta = await _servicio.Create(data);

            _logger.LogInformation($"Salida al endpoint Create");

            return respuesta;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([Required] string id, [FromBody] ApiObjectCreateModel data) 
        {
            _logger.LogInformation($"Entrada al endpoint Update con los siguientes parametros: {JsonConvert.SerializeObject(data)} y el id: {id}");

            ActionResult respuesta = await _servicio.Update(id, data);

            _logger.LogInformation($"Salida al endpoint Update");

            return respuesta;
        }
    }
}
