using System.ComponentModel.DataAnnotations;

namespace Prueba_Tecnica_Kaprielian.Modelos
{
    public class RandomUser
    {
        [Required(ErrorMessage = "Name es requerido.")]
        public Name Name { get; set; }

        [Required(ErrorMessage = "Login es requerido.")]
        public Login Login { get; set; }
    }

    public class Name
    {
        [Required(ErrorMessage = "Title es requerido.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Title debe tener entre 3 y 50 caracteres.")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "First es requerido.")]
        [StringLength(50, ErrorMessage = "First no puede superar los 50 caracteres.")]
        public string First { get; set; } = "";

        [Required(ErrorMessage = "Last es requerido.")]
        [StringLength(50, ErrorMessage = "Last no puede superar los 50 caracteres.")]
        public string Last { get; set; } = "";
    }

    public class Login
    {
        [Required(ErrorMessage = "Uuid es requerido.")]
        public string Uuid { get; set; } = "";

        [Required(ErrorMessage = "Username es requerido.")]
        [StringLength(50, ErrorMessage = "Username no puede superar los 50 caracteres.")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Password es requerido.")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password debe tener al menos 6 caracteres.")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Salt es requerido.")]
        public string Salt { get; set; } = "";

        [Required(ErrorMessage = "Md5 es requerido.")]
        public string Md5 { get; set; } = "";

        [Required(ErrorMessage = "Sha1 es requerido.")]
        public string Sha1 { get; set; } = "";

        [Required(ErrorMessage = "Sha256 es requerido.")]
        public string Sha256 { get; set; } = "";
    }
}
