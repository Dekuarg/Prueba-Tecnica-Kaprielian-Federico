using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Prueba_Tecnica_Kaprielian.Helper;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.ComponentModel.DataAnnotations;
using static System.Net.WebRequestMethods;

namespace Prueba_Tecnica_Kaprielian.Services
{
    public class ApiObject(HttpClient httpClient) : IApiObject
    {
        public async Task<ActionResult> Create(ApiObjectCreateModel data)
        {
            var (result, response) = ValidationsHelper.ValidateObject(data);

            if (result)
                return response;

            var ver = await httpClient.PostAsJsonAsync("objects", data);
            var body = await ver.Content.ReadAsStringAsync();
            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        public async Task<ActionResult> GetAll()
        {
            var ver = await httpClient.GetAsync("objects");
            var body = await ver.Content.ReadAsStringAsync();
            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        public async Task<ActionResult> GetById(string id)
        {
            var (result,response) = ValidationsHelper.ValidateId(id);

            if (result)
                return response;
            
            var ver = await httpClient.GetAsync($"objects/{id}");
            var body = await ver.Content.ReadAsStringAsync();
            return new ContentResult { Content = body, ContentType = "application/json" };
        }

        public async Task<ActionResult> Update(string id, ApiObjectCreateModel data)
        {
            var (isIdInvalid, idResponse) = ValidationsHelper.ValidateId(id);
            if (isIdInvalid)
                return idResponse;

            var (isObjectInvalid, objectResponse) = ValidationsHelper.ValidateObject(data);
            if (isObjectInvalid)
                return objectResponse;


            var ver = await httpClient.PutAsJsonAsync($"objects/{id}", data);
            var body = await ver.Content.ReadAsStringAsync();
            return new ContentResult { Content = body, ContentType = "application/json" };
        }
    }
}
