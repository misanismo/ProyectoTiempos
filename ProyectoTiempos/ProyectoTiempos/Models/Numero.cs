using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Numero
    {
        [Key]
        public int IdNumero { get; set; }

        public int Numeros { get; set; }
    }
}