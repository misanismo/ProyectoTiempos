using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProyectoTiempos.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<Numero> Numeros { get; set; }

        public System.Data.Entity.DbSet<Sorteo> Sorteos { get; set; }

        public System.Data.Entity.DbSet<Apuesta> Apuestas { get; set; }

        public System.Data.Entity.DbSet<Casa> Casas { get; set; }

        public System.Data.Entity.DbSet<NumeroGanador> NumerosGanadores { get; set; }

        public System.Data.Entity.DbSet<Premio> Premios { get; set; }
    }
}