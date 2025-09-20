using Dominio.Modelos;
using Dominio.ModelosDB;
using FluentValidation;
using Infraestructura.CarpetaPrincipal;
using Logica.Validaciones;
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
        public async Task<ActionResult> GetAll() 
        {  

            ActionResult respuesta = await _servicio.GetAll();

            return respuesta;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([Required] string id) 
        {

            ActionResult respuesta = await _servicio.GetById(id);

            return respuesta;
        }

        [HttpPost]
        public async Task<ActionResult> Create([FromBody][Required] ApiObjectCreateModel data) 
        {

            ActionResult respuesta = await _servicio.Create(data);

            return respuesta;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update([Required] string id, [FromBody] ApiObjectCreateModel data) 
        {

            ActionResult respuesta = await _servicio.Update(id, data);

            return respuesta;
        }
    }
}
