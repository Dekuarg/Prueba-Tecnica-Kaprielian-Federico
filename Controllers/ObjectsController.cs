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
        public ObjectsController(IApiObject servicio)
        {
            _servicio = servicio;
        }
        [HttpGet] 
        public async Task<ActionResult> GetAll() => await _servicio.GetAll();


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([Required] string id) => await _servicio.GetById(id);


        [HttpPost]
        public async Task<ActionResult> Create([FromBody][Required] ApiObjectCreateModel data) => await _servicio.Create(data);


        [HttpPut("{id}")]
        public async Task<ActionResult> Update([Required] string id, [FromBody] ApiObjectCreateModel data) => await _servicio.Update(id, data);

    }
}
