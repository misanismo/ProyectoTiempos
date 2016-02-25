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

        public TipoUsuarioWrapper TipoUsuario { get; set; }

        public bool Estado { get; set; }

        public string Correo { get; set; }

        }
    public enum TipoUser
    {
        Admin = 1,
        Cliente = 2
    }
    public class TipoUsuarioWrapper
    {
        private TipoUser _t;
        public int Value
        {
            get
            {
                return (int)_t;
            }
            set
            {
                _t = (TipoUser)value;
            }
        }
        public TipoUser EnumValue
        {
            get
            {
                return _t;
            }
            set
            {
                _t = value;
            }
        }

        public static implicit operator TipoUsuarioWrapper(TipoUser p)
        {
            return new TipoUsuarioWrapper { EnumValue = p };
        }

        public static implicit operator TipoUser(TipoUsuarioWrapper pw)
        {
            if (pw == null) return TipoUser.Cliente;
            return pw.EnumValue;
        }
    }
}