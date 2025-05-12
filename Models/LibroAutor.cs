using System.ComponentModel.DataAnnotations;

namespace GestionBiblioteca.Models
{
    public class LibroAutor
    {
        [Required(ErrorMessage = "El código del libro es obligatorio")]
        [Display(Name = "Código del Libro")]
        public string CodLibro { get; set; }

        [Required(ErrorMessage = "El código del autor es obligatorio")]
        [Display(Name = "Código del Autor")]
        public string CodAutor { get; set; }
    }
}
