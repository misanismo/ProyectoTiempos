using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class DetalleApuestaViewModel
    {
        public int IdNumero { get; set; }
        public int Numeros { get; set; }
        public double Monto { get; set; }
        
        public string ErrorDescription { get; set; }

        public int ErrorCode { get; set; }

        public int Borrar { get; set; }

    }
}