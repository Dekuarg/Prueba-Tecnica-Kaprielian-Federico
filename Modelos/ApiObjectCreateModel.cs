using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Prueba_Tecnica_Kaprielian.Modelos
{
    public class ApiObjectCreateModel
    {
        [Required]
        public string Id { get; set; } = "";
        [Required]
        public string Name { get; set; } = "";
        [Required]
        public ObjectData Data { get; set; } = new();
        public class ObjectData
        {
            public int Year { get; set; }
            public decimal Price { get; set; }

            [JsonPropertyName("CPU model")]
            public string CpuModel { get; set; } = "";

            [JsonPropertyName("Hard disk size")]
            public string HardDiskSize { get; set; } = "";
        }
    }
}
