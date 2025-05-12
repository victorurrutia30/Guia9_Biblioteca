using System.ComponentModel.DataAnnotations;

namespace GestionBiblioteca.Models
{
    public class Libros
    {
        [Required(ErrorMessage = "El código del libro es obligatorio")]
        [Display(Name = "Código del Libro")]
        public string CodLibro { get; set; }

        [Required(ErrorMessage = "El título del libro es obligatorio")]
        [Display(Name = "Título")]
        public string Titulo { get; set; }

        [Display(Name = "Editorial")]
        public string Editorial { get; set; }

        [Display(Name = "Año de Publicación")]
        [Range(1000, 9999, ErrorMessage = "Ingrese un año válido (formato: AAAA)")]
        public int? AnioPublicacion { get; set; }

        [Display(Name = "ISBN")]
        public string ISBN { get; set; }

        [Required(ErrorMessage = "La fecha de ingreso es obligatoria")]
        [Display(Name = "Fecha de Ingreso")]
        [DataType(DataType.Date)]
        public DateTime FechaIngreso { get; set; }
        public List<string> NombresAutores { get; set; } = new List<string>();
    }
}
