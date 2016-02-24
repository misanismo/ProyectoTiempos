using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Apuesta
    {
        public int Id { get; set; }

        public int IdUsuario { get; set; }
        public virtual Usuario Usuario { get; set; }

        public int IdNumero { get; set; }
        public virtual Numero Numero { get; set; }

        public int IdSorteo { get; set; }
        public virtual Sorteo Sorteo { get; set; }

        public double MontoApuesta { get; set; }

        monto


    }
}