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
    public class ApuestasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();



        // GET: Apuestas
        public ActionResult Index()
        {
            var apuestas = db.Apuestas.Include(a => a.Numero).Include(a => a.Sorteo).Include(a => a.Usuario);
            return View(apuestas.ToList());
        }

        // GET: Apuestas/Details/5
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

        // GET: Apuestas/Create
        public ActionResult Create()
        {
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "Numeros");
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre");
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre");
            return View();
        }

        // POST: Apuestas/Create
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

            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "Numeros", apuesta.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
        }

        // GET: Apuestas/Edit/5
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
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "Numeros", apuesta.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
        }

        // POST: Apuestas/Edit/5
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
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "Numeros", apuesta.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", apuesta.IdSorteo);
            ViewBag.IdUsuario = new SelectList(db.Usuarios, "IdUsuario", "Nombre", apuesta.IdUsuario);
            return View(apuesta);
        }

        // GET: Apuestas/Delete/5
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

        // POST: Apuestas/Delete/5
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
    }
}
