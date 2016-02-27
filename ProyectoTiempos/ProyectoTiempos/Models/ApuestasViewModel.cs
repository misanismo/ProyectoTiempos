using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class ApuestasViewModel
    {
        

        public int IdUsuario { get; set; }
        

        public int IdNumero { get; set; }


        public int Numero { get; set; }

        public IList<Numero> Numeros { get; set; } 

        public int IdSorteo { get; set; }
        public string  Sorteo { get; set; }

        public double MontoApuesta { get; set; }

        public IList<DetalleApuestaViewModel> Detalles { get; set; }



    }
}