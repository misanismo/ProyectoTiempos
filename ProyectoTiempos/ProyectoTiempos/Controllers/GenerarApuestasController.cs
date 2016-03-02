using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using ProyectoTiempos.Clases;
using ProyectoTiempos.Models;

namespace ProyectoTiempos.Controllers
{
    [AuthorizeUser(Roles = TipoUser.Cliente | TipoUser.Admin)]
    public class GenerarApuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: GenerarApuestas
        public ActionResult Index()
        {
            var user = (CustomPrincipal)HttpContext.User;
            var apuestas = db.Apuestas.Where(a=>a.IdUsuario ==user.UsuarioId).Include(a => a.Sorteo).Include(a => a.Usuario);
            return View(apuestas.ToList());
        }

        // GET: GenerarApuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }
            return View(apuesta);
        }

        // GET: GenerarApuestas/Create
        public ActionResult Create()
        {
            ApuestasViewModel apuestas = new ApuestasViewModel();
            apuestas.Detalles = new List<DetalleApuestaViewModel>();
            apuestas.Numeros = db.Numeros.ToList();
            ViewBag.IdSorteo = new SelectList(db.Sorteos.Where(a=>a.Estado), "IdSorteo", "Nombre");
            return View(apuestas);
        }

        // POST: GenerarApuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(IList<string> jsonDetallesList, int? idUsuario, int? idSorteo)
        {
            IEnumerable<DetalleApuestaViewModel> detalleList = new JavaScriptSerializer().Deserialize<IList<DetalleApuestaViewModel>>(jsonDetallesList[0]);

            double totalApuesta = detalleList.Sum(a => a.Monto);



            // LOGICA AQUI <-------------

            //if (ModelState.IsValid)
            //{
            //    db.Apuestas.Add(apuesta);
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}

            //ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", apuesta.IdNumero);
            //ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            //ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View();
        }

        [HttpPost]
        public ActionResult GenerarApuesta(IList<string> jsonDetallesList, int? idUsuario, int? idSorteo)
        {
            try
            {
                IEnumerable<DetalleApuestaViewModel> listaapuestas = new JavaScriptSerializer().Deserialize<IList<DetalleApuestaViewModel>>(jsonDetallesList[0]);

                var detalleList = listaapuestas.Where(a => a.Borrar == 0).OrderByDescending(a => a.Monto);

                var sorteo= db.Sorteos.Find(idSorteo);

                if (sorteo.FechaInicio <= DateTime.Now && DateTime.Now >= sorteo.FechaIFinal)
                {
                    return Json(new { Error = -1, Message = "El sorteo ya esta Vencido, por favor seleccione otro sorteo" });
                }
                

                double montoTabla = 0;
                double montoApuesta = 0;
                double total1 = 0, total2 = 0, total3 = 0;
                int count = 1;
                bool comprometido = false;
                double totalCompremetido = 0;
                
                double totalApuesta = detalleList.Sum(a => a.Monto);
                double totalCasa = db.Casas.Where(a => a.IdCasa == 1).Sum(a => a.MontoAcumulado) + totalApuesta;

                var apuestaList = db.Apuestas.OrderByDescending(a => a.IdApuesta).ToList();

                IList<DetalleApuesta> tablaAcumulada = new List<DetalleApuesta>();
                tablaAcumulada = db.DetalleApuestas.Where(a=>a.Apuesta.IdSorteo == idSorteo).OrderByDescending(a => a.Monto).ToList();

                foreach (var item in detalleList)
                {
                    double montoTotalAp = 0;
                    montoApuesta = item.Monto;
                    montoTabla = tablaAcumulada.Where(a => a.IdNumeros == item.IdNumero).Sum(a => a.Monto);
                    if (montoTabla > 0 || montoTabla != null)
                    {
                        montoTotalAp = montoApuesta + montoTabla;
                        if (count == 1)
                        {
                            total1 += montoTotalAp * 60;
                            count++;
                        }else if (count == 2)
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
                }
                totalCompremetido = total1 + total2 + total3;
                if (totalCasa > totalCompremetido || totalCasa == totalCompremetido)
                {
                    comprometido = false;
                }
                else
                {
                    comprometido = true;
                }

                if (!comprometido)
                {
                    var apuesta = new Apuesta();
                    apuesta.IdSorteo = (int) idSorteo;
                    apuesta.IdUsuario = (int) idUsuario;
                    apuesta.MontoApuesta = totalApuesta;
                    db.Apuestas.Add(apuesta);
                    db.SaveChanges();

                    foreach (var item in detalleList)
                    {
                        var detalle = new DetalleApuesta();
                        detalle.IdApuesta = apuestaList.FirstOrDefault().IdApuesta + 1;
                        detalle.IdNumeros = item.IdNumero;
                        detalle.Monto = (int) item.Monto;

                        db.DetalleApuestas.Add(detalle);
                        db.SaveChanges();
                    }

                    var updateCasa = new Casa();
                    updateCasa.MontoAcumulado = totalCasa;
                    updateCasa.IdCasa = 1;
                    db.Entry(updateCasa).State = EntityState.Modified;


                }
                else
                {
                    return Json(new { Error = -1, Message = "El monto de su apuesta exede el capital de la casa, por favor baje los montos" });
                }
                IList<DetalleApuestaViewModel> newList = new List<DetalleApuestaViewModel>();
                return PartialView("_detalleApuestas", newList);
            }
            catch (Exception)
            {
                return Json(new { Error = -1, Message = "Error al Agregar la Apuesta" });
            }
        }


        // GET: GenerarApuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }
            
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
        }

        // POST: GenerarApuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdApuesta,IdUsuario,IdNumero,IdSorteo,MontoApuesta")] Apuesta apuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(apuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
        }

        // GET: GenerarApuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Apuesta apuesta = db.Apuestas.Find(id);
            if (apuesta == null)
            {
                return HttpNotFound();
            }
            return View(apuesta);
        }

        // POST: GenerarApuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Apuesta apuesta = db.Apuestas.Find(id);
            db.Apuestas.Remove(apuesta);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // Convertimos el Json en una lista con los detalles de la apuesta 
        // Luego con el render borramos la opcion elegida y refrescamos la tabla...

        [HttpPost]
        public ActionResult RenderListDetallesOrden(IList<string> jsonDetallesList)
        {
            try
            {
                IEnumerable<DetalleApuestaViewModel> detalleList = new JavaScriptSerializer().Deserialize<IList<DetalleApuestaViewModel>>(jsonDetallesList[0]);
                IList<DetalleApuestaViewModel> newList = new List<DetalleApuestaViewModel>();
                foreach (var item in detalleList)
                {
                    if (item.Borrar == 1)
                        item.IdNumero = -1;

                    newList.Add(item);
                }
                return PartialView("_detalleApuestas", newList);
            }
            catch (Exception)
            {
                return Json(new { Error = -1, Message = "Error al Agregar/Modificar la Orden" });
            }
        }


        // Agrega el detalle a la lista y refresca solo la tabla ...

        [HttpPost]
        public ActionResult AddAndRenderListDetallesOrden(IList<string> jsonDetallesList, int? idNumero, int? numero, int? montoApuesta)
        {
            try
            {
                IEnumerable<DetalleApuestaViewModel> detalleList = new JavaScriptSerializer().Deserialize<IList<DetalleApuestaViewModel>>(jsonDetallesList[0]);

                if (detalleList.Any(item => item.IdNumero == idNumero))
                {
                    return Json(new { Error = -1, Message = "Ya existe una entrada para el numero " + numero + " por favor ingrese correctamente la apuesta." });
                }

                if (montoApuesta == null || montoApuesta <= 0)
                    return Json(new { Error = -1, Message = "Por Favor indique monto correctamente." });

                DetalleApuestaViewModel detalle = new DetalleApuestaViewModel();
                detalle.IdNumero = (int)idNumero;
                detalle.Numeros = (int)numero;
                detalle.Monto = (int)montoApuesta;
                detalle.Borrar = 0;
                detalle.ErrorDescription = "";
                detalle.ErrorCode = 0;

                //Agregamos a la lista 

                IList<DetalleApuestaViewModel> newList = new List<DetalleApuestaViewModel>();
                int index = 0;
                foreach (var item in detalleList)
                {
                    newList.Add(item);
                    index++;
                }

                newList.Add(detalle);

                return PartialView("_detalleApuestas", newList);
            }
            catch (Exception)
            {
                return Json(new { Error = -1, Message = "Error al Agregar la Apuesta" });
            }
        }

    }

}
