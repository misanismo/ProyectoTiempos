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
    public class NumeroGanadorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NumeroGanadors
        public ActionResult Index()
        {
            var numerosGanadores = db.NumerosGanadores.Include(n => n.Numero).Include(n => n.Sorteo);
            return View(numerosGanadores.ToList());
        }

        // GET: NumeroGanadors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumeroGanador numeroGanador = db.NumerosGanadores.Find(id);
            if (numeroGanador == null)
            {
                return HttpNotFound();
            }
            return View(numeroGanador);
        }

        // GET: NumeroGanadors/Create
        public ActionResult Create()
        {
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero");
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre");
            return View();
        }

        // POST: NumeroGanadors/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNumeroGanador,IdSorteo,IdNumero")] NumeroGanador numeroGanador)
        {
            if (ModelState.IsValid)
            {
                db.NumerosGanadores.Add(numeroGanador);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", numeroGanador.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", numeroGanador.IdSorteo);
            return View(numeroGanador);
        }

        // GET: NumeroGanadors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumeroGanador numeroGanador = db.NumerosGanadores.Find(id);
            if (numeroGanador == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", numeroGanador.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", numeroGanador.IdSorteo);
            return View(numeroGanador);
        }

        // POST: NumeroGanadors/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNumeroGanador,IdSorteo,IdNumero")] NumeroGanador numeroGanador)
        {
            if (ModelState.IsValid)
            {
                db.Entry(numeroGanador).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdNumero = new SelectList(db.Numeros, "IdNumero", "IdNumero", numeroGanador.IdNumero);
            ViewBag.IdSorteo = new SelectList(db.Sorteos, "IdSorteo", "Nombre", numeroGanador.IdSorteo);
            return View(numeroGanador);
        }

        // GET: NumeroGanadors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NumeroGanador numeroGanador = db.NumerosGanadores.Find(id);
            if (numeroGanador == null)
            {
                return HttpNotFound();
            }
            return View(numeroGanador);
        }

        // POST: NumeroGanadors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NumeroGanador numeroGanador = db.NumerosGanadores.Find(id);
            db.NumerosGanadores.Remove(numeroGanador);
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
