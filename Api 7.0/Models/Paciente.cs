using System.ComponentModel.DataAnnotations;

namespace Api_7._0.Models
{
    public class Paciente
    {
        [Key]
        public int IDPaciente { get; set; }
        [Required]

        public string Nombre { get; set; }
        [Required]

        public int Edad { get; set; }
        [Required]

        public string Genero { get; set; }
        [Required]

        public string Direccion { get; set; }
        [Required]

        public string Telefono { get; set; }
    }
}
