using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Premio
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public int Multiveces { get; set; }
        //cantidad de veces x las q se multiplica la apuesta
    }
}