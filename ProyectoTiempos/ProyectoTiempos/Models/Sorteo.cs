using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Sorteo
    {
        [Key]
        public int  IdSorteo { get; set; }

        public string Nombre { get; set; }

        public int NumeroSorteo { get; set; }

        public DateTime Hora { get; set; }

        public DateTime Fecha { get; set; }

        public bool Estado { get; set; }



    }
}