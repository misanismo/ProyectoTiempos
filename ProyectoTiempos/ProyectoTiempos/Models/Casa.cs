using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Casa
    {
        [Key]
        public int IdCasa { get; set; }

        public string Nombre { get; set; }

        public double MontoInicial { get; set; }

        public double MontoAcumulado { get; set; }


    }
}