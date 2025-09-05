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
        public ObjectData Data { get; set; } = new();
        public class ObjectData
        {
            public int Year { get; set; }
            public decimal Price { get; set; }

            [JsonPropertyName("CPU model")]
            public string CpuModel { get; set; } = "";

            [JsonPropertyName("Hard disk size")]
            public string HardDiskSize { get; set; } = "";

            public string Color { get; set; } = "";

            public string Capacity { get; set; } = "";

            public string Generation { get; set; } = "";

            [JsonPropertyName("Case Size")]
            public string CaseSize { get; set; } = "";
            public string Description { get; set; } = "";

            [JsonPropertyName("Screen Size")]
            public string ScreenSize { get; set; } = "";
        }
    }
}
