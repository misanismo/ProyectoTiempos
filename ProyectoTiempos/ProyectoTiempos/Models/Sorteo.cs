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

        public TimeSpan HoraInicio { get; set; }

        public DateTime FechaInicio { get; set; }

        public TimeSpan HoraFinal { get; set; }

        public DateTime FechaIFinal { get; set; }

        public bool Estado { get; set; }



    }
}