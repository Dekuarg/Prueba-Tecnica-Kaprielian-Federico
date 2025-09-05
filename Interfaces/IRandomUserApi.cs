using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Kaprielian.Modelos;

namespace Prueba_Tecnica_Kaprielian.Interfaces
{
    public interface IRandomUserApi
    {
        Task<ActionResult> GetRandom();
        Task<ActionResult> SaveRandom(RandomUser data);
    }
}
