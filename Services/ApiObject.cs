using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Controllers;
using Prueba_Tecnica_Kaprielian.Helper;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Kaprielian.Services
{
    public class ApiObject : IApiObject
    {
        private readonly ILogger<ApiObject> _logger;
        private readonly HttpClient _client;
        public ApiObject(HttpClient client, ILogger<ApiObject> logger)
        {
            _client = client;
            _logger = logger;
        }

        #region Metodos

        /// <summary>
        /// Crea un nuevo objeto enviando los datos proporcionados al API.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ActionResult> Create(ApiObjectCreateModel data)
        {
            var (result, response) = ValidationsHelper.ValidateObject(data);

            if (result)
                return response;

            var request = await _client.PostAsJsonAsync("objects", data);
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo Create: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        /// <summary>
        /// Obtiene todos los objetos existentes
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> GetAll()
        {
            var request = await _client.GetAsync("objects");
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo GetAll: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        /// <summary>
        /// Obtiene un objeto específico por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ActionResult> GetById(string id)
        {
            var (result, response) = ValidationsHelper.ValidateId(id);

            if (result)
                return response;

            var request = await _client.GetAsync($"objects/{id}");
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo GetById: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        /// <summary>
        /// Actualiza un objeto existente identificado por su Id con los nuevos datos proporcionados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public async Task<ActionResult> Update(string id, ApiObjectCreateModel data)
        {
            var (isIdInvalid, idResponse) = ValidationsHelper.ValidateId(id);
            if (isIdInvalid)
                return idResponse;

            var (isObjectInvalid, objectResponse) = ValidationsHelper.ValidateObject(data);
            if (isObjectInvalid)
                return objectResponse;

            var request = await _client.PutAsJsonAsync($"objects/{id}", data);
            var body = await request.Content.ReadAsStringAsync();

            _logger.LogInformation($"Se detecto una respuesta del metodo Update: {body}");

            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        #endregion

    }
}
