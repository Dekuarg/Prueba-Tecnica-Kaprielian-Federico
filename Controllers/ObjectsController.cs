using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Kaprielian.Controllers
{
    [ApiController]
    [Route("api/objects")]
    public class ObjectsController(IApiObject servicio) : ControllerBase
    {
        [HttpGet] 
        public async Task<IActionResult> GetAll() => await servicio.GetAll();

        [HttpGet("{id}")] 
        public async Task<IActionResult> GetById([Required] string id ) => await servicio.GetById(id);

        [HttpPost] 
        public async Task<IActionResult> Create([FromBody][Required] ApiObjectCreateModel data ) => await servicio.Create(data);

        [HttpPut("{id}")] 
        public async Task<IActionResult> Update([Required] string id, [FromBody] ApiObjectCreateModel data ) => await servicio.Update(id, data);
    }
}
