using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProyectoTiempos.Models;

namespace ProyectoTiempos.Controllers
{
    public class DetalleApuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DetalleApuestas
        public ActionResult Index()
        {
            var detalleApuestas = db.DetalleApuestas.Include(d => d.Apuesta);
            return View(detalleApuestas.ToList());
        }

        // GET: DetalleApuestas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleApuesta detalleApuesta = db.DetalleApuestas.Find(id);
            if (detalleApuesta == null)
            {
                return HttpNotFound();
            }
            return View(detalleApuesta);
        }

        // GET: DetalleApuestas/Create
        public ActionResult Create()
        {
            ViewBag.IdApuesta = new SelectList(db.Apuestas, "IdApuesta", "IdApuesta");
            return View();
        }

        // POST: DetalleApuestas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdDetalleApuesta,IdApuesta,IdNumeros,Monto")] DetalleApuesta detalleApuesta)
        {
            if (ModelState.IsValid)
            {
                db.DetalleApuestas.Add(detalleApuesta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdApuesta = new SelectList(db.Apuestas, "IdApuesta", "IdApuesta", detalleApuesta.IdApuesta);
            return View(detalleApuesta);
        }

        // GET: DetalleApuestas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleApuesta detalleApuesta = db.DetalleApuestas.Find(id);
            if (detalleApuesta == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdApuesta = new SelectList(db.Apuestas, "IdApuesta", "IdApuesta", detalleApuesta.IdApuesta);
            return View(detalleApuesta);
        }

        // POST: DetalleApuestas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdDetalleApuesta,IdApuesta,IdNumeros,Monto")] DetalleApuesta detalleApuesta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detalleApuesta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdApuesta = new SelectList(db.Apuestas, "IdApuesta", "IdApuesta", detalleApuesta.IdApuesta);
            return View(detalleApuesta);
        }

        // GET: DetalleApuestas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetalleApuesta detalleApuesta = db.DetalleApuestas.Find(id);
            if (detalleApuesta == null)
            {
                return HttpNotFound();
            }
            return View(detalleApuesta);
        }

        // POST: DetalleApuestas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetalleApuesta detalleApuesta = db.DetalleApuestas.Find(id);
            db.DetalleApuestas.Remove(detalleApuesta);
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
    }
}
