using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class NumeroGanador
    {
        [Key]
        public int IdNumeroGanador { get; set; }

        public int IdSorteo { get; set; }
        public virtual Sorteo Sorteo { get; set; }

        public int IdNumero { get; set; }
        public virtual Numero Numero { get; set; }







    }
}