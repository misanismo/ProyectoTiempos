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
            var apuestas = db.Apuestas.Include(a => a.Numero).Include(a => a.Sorteo).Include(a => a.Usuario);
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
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre");
            return View(apuestas);
        }

        // POST: GenerarApuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdApuesta,IdUsuario,IdNumero,IdSorteo,MontoApuesta")] Apuesta apuesta)
        {
            if (ModelState.IsValid)
            {
                db.Apuestas.Add(apuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", apuesta.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
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
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", apuesta.IdNumero);
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
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", apuesta.IdNumero);
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

        [HttpPost]
        public ActionResult AddAndRenderListDetallesOrden(IList<string> jsonDetallesList,  int? idNumero, int? numero, int? montoApuesta)
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
