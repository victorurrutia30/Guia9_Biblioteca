using System.ComponentModel.DataAnnotations;

namespace GestionBiblioteca.Models
{
    public class Usuario
    {

        //Definimos las propiedades asociadas a los campos de la tabla "Usuarios"
    [Display(Name = "Código de Usuario")]
    public string CodUsuario { get; set; }
    public string Nombre { get; set; }
        
    [Display(Name = "Usuario del sistema")]
    public string Username { get; set; }
    public string Password { get; set; }
        [Display(Name = "Fecha de creación")]
        public string FechaCreacion { get; set; }

}
}
