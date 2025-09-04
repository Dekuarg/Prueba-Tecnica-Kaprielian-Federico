using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Modelos;

namespace Prueba_Tecnica_Kaprielian.Helper
{
    public class ValidationsHelper
    {
        public static (bool,ActionResult) ValidateId(string id)
        {

            if(!string.IsNullOrEmpty(id))
                return (false, null);


                ErrorResponse error = new()
                {
                    Status = 400,
                    Title = "Error en el envio de parametro id",
                    Description = "El id no puede ser nulo o vacio",
                    TraceId = Guid.NewGuid().ToString()
                };

                return (true,new ContentResult { Content = JsonConvert.SerializeObject(error), ContentType = "application/json" });
            
        }

        public static (bool, ActionResult) ValidateObject(ApiObjectCreateModel data)
        {
            if ( !string.IsNullOrEmpty(data.Name) && !string.IsNullOrEmpty(data.Id))
                return (false, null);


            ErrorResponse error = new()
            {
                Status = 400,
                Title = "Error en el envio de parametro id",
                Description = "Los atributos Id y Nombre son requeridos",
                TraceId = Guid.NewGuid().ToString()
            };

            return (true, new ContentResult { Content = JsonConvert.SerializeObject(error), ContentType = "application/json" });
        }


    }
}
