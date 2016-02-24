using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class Usuario
    { 
        [Key]
        public int IdUsuario { get; set; }

        public string Nombre { get; set; }

        public string Apellidos { get; set; }

        public string NombreUsuario { get; set; }

        public string Contraseña { get; set; }

        public enum TipoUser
        {
            Admin = 1,
            Cliente = 2
        }

        public bool Estado { get; set; }

        public string Correo { get; set; }




    }
}