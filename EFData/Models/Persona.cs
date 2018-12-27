using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFData.Models
{
    public class Persona
    {
        [Key]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad { get; set; }

        public string DocumentoIdentificacion { get; set; }
    }
}
