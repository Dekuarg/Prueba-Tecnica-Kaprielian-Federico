using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Kaprielian.Modelos;

namespace Prueba_Tecnica_Kaprielian.Interfaces
{
    public interface IApiObject
    {
        Task<ActionResult> GetAll();
        Task<ActionResult> GetById(string id);
        Task<ActionResult> Create(ApiObjectCreateModel data);
        Task<ActionResult> Update(string id, ApiObjectCreateModel data);
    }
}
