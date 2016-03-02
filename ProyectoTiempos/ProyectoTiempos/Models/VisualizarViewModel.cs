using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class VisualizarViewModel
    {
        public IList<Sorteo> Sorteos { get; set; }

        public IList<DetalleApuesta>  DetalleApuestas { get; set; }
    }
}