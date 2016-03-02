using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTiempos.Clases;
using ProyectoTiempos.Models;

namespace ProyectoTiempos.Controllers
{
    [AuthorizeUser(Roles = TipoUser.Admin)]
    public class ConfiguracionController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Configuracion
        public ActionResult Index()
        {
            var detalles = db.DetalleApuestas.OrderByDescending(a=>a.Monto).ToList();
            ConfiguracionViewModel config = new ConfiguracionViewModel();
            config.MontoAcumulado = db.Casas.ToList().FirstOrDefault().MontoAcumulado;
            config.MontoAcumulado += detalles.Sum(a => a.Monto);
            config.MontoEnElPeorDeLosCasos = MontoPeorCaso(detalles);
            double montoMejor = MontoMejorDeLosCasos(detalles);
            config.GananciaMinima = config.MontoAcumulado - config.MontoEnElPeorDeLosCasos;
            config.GananciaMaxima = config.MontoAcumulado - montoMejor;
            return View(config);
        }


        //Logica para sacar el peor de los casos

        public double MontoPeorCaso(IList<DetalleApuesta> detalleList)
        {
            try
            {
                double montoTabla = 0;
                double montoApuesta = 0;
                double total1 = 0, total2 = 0, total3 = 0;
                int count = 1;
                bool comprometido = false;
                double totalCompremetido = 0;

                double totalApuesta = detalleList.Sum(a => a.Monto);
                double totalCasa = db.Casas.Where(a => a.IdCasa == 1).Sum(a => a.MontoAcumulado) + totalApuesta;

                foreach (var item in detalleList)
                {
                    double montoTotalAp = 0;
                    montoApuesta = item.Monto;
                    montoTotalAp = montoApuesta + montoTabla;
                    if (count == 1)
                    {
                        total1 += montoTotalAp * 60;
                        count++;
                    }
                    else if (count == 2)
                    {
                        total2 += montoTotalAp * 10;
                        count++;
                    }
                    else if (count == 3)
                    {
                        total3 += montoTotalAp * 5;
                        count++;
                    }
                }
                totalCompremetido = total1 + total2 + total3;
                return totalCompremetido;
                
            }
            catch (Exception)
            {

            }
            return 0;
        }

        public double MontoMejorDeLosCasos(IList<DetalleApuesta> detalleList)
        {
            try
            {
                
                double montoTabla = 0;
                double montoApuesta = 0;
                double total1 = 0, total2 = 0, total3 = 0;
                int count = 1;
                bool comprometido = false;
                double totalCompremetido = 0;

                double totalApuesta = detalleList.Sum(a => a.Monto);
                double totalCasa = db.Casas.Where(a => a.IdCasa == 1).Sum(a => a.MontoAcumulado) + totalApuesta;

                foreach (var item in detalleList.OrderBy(a=>a.Monto))
                {
                    double montoTotalAp = 0;
                    montoApuesta = item.Monto;
                    montoTotalAp = montoApuesta + montoTabla;
                    if (count == 1)
                    {
                        total1 += montoTotalAp * 60;
                        count++;
                    }
                    else if (count == 2)
                    {
                        total2 += montoTotalAp * 10;
                        count++;
                    }
                    else if (count == 3)
                    {
                        total3 += montoTotalAp * 5;
                        count++;
                    }
                }
                totalCompremetido = total1 + total2 + total3;
                return totalCompremetido;

            }
            catch (Exception)
            {

            }
            return 0;
        }

        // GET: Configuracion/Edit/5


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
