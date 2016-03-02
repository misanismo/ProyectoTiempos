using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Apuesta
    {
        [Key]
        public int IdApuesta { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int IdSorteo { get; set; }
        public virtual Sorteo Sorteo { get; set; }

        public double MontoApuesta { get; set; }

        public ICollection<DetalleApuesta> DetalleApuestas { get; set; }

     
    }
}