using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class DetalleApuesta
    {
        [Key]
        public int IdDetalleApuesta { get; set; }

        public int IdApuesta { get; set; }
        public virtual Apuesta Apuesta { get; set; }

        public int IdNumeros { get; set; }
        public virtual Numero Numero { get; set; }
        
        public int Monto { get; set; }
        
    }
}