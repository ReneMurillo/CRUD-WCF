using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WSApplicationWeb.Models
{
    public class PersonaView
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public int Edad { get; set; }

        public string DocumentoIdentificacion { get; set; }
    }
}