using Microsoft.AspNetCore.Mvc;
using Prueba_Tecnica_Kaprielian.Controllers;
using Prueba_Tecnica_Kaprielian.Helper;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.Text;
using System.Text.Json;

namespace Prueba_Tecnica_Kaprielian.Services
{
    public class ApiRandomUser : IRandomUserApi
    {
        private readonly ILogger<ApiRandomUser> _logger;
        private readonly HttpClient _client;
        public ApiRandomUser(HttpClient client, ILogger<ApiRandomUser> logger)
        {
            _client = client;
            _logger = logger;
        }
        #region Metodos

        /// <summary>
        /// Metodo que se usa para obtener un usario random
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetRandom()
        {
            var request = await _client.GetAsync("https://randomuser.me/api");
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo GetRandom: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        /// <summary>
        /// Metodo que utlizamos para guardar un usuario random
        /// Las validaciones de data se encuentran en la misma clase (DataAnottations)
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ActionResult> SaveRandom(RandomUser data)
        {
            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var request = await _client.PostAsync("https://webhook.link/dddb9d9d-c53a-415c-9d6f-2a44559afa50", content);
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo SaveRandom: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };

        }

        #endregion

    }
}
