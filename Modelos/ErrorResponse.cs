namespace Prueba_Tecnica_Kaprielian.Modelos
{
    public class ErrorResponse
    {
        public string Title { get; set; } = "";
        public int Status { get; set; }
        public string Description { get; set; } = "";
        public string TraceId { get; set; } = "";
    }
}
