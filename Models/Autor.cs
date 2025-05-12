using System.ComponentModel.DataAnnotations;

namespace GestionBiblioteca.Models
{
    public class Autor
    {
        [Required(ErrorMessage = "El código es obligatorio")]
        [Display(Name = "Código del Autor")]
        public string CodAutor { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre del Autor")]
        public string NombreAutor { get; set; }

        [Display(Name = "Nacionalidad")]
        public string Nacionalidad { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }
    }
}
