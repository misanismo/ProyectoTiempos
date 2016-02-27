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
    public class NumeroesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Numeroes
        public ActionResult Index()
        {
            return View(db.Numeros.ToList());
        }

        // GET: Numeroes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numero numero = db.Numeros.Find(id);
            if (numero == null)
            {
                return HttpNotFound();
            }
            return View(numero);
        }

        // GET: Numeroes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Numeroes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdNumero,Numeros")] Numero numero)
        {

            if (ModelState.IsValid)
            {
                for (int i = 0; i < 100; i++)
                {
                    var numeross = new Numero();
                    numeross.Numeros = i;
                    db.Numeros.Add(numeross);
                    db.SaveChanges();
                }
               
                return RedirectToAction("Index");
            }

            return View(numero);
        }

        // GET: Numeroes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numero numero = db.Numeros.Find(id);
            if (numero == null)
            {
                return HttpNotFound();
            }
            return View(numero);
        }

        // POST: Numeroes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdNumero,Numeros")] Numero numero)
        {
            if (ModelState.IsValid)
            {
                db.Entry(numero).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(numero);
        }

        // GET: Numeroes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Numero numero = db.Numeros.Find(id);
            if (numero == null)
            {
                return HttpNotFound();
            }
            return View(numero);
        }

        // POST: Numeroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Numero numero = db.Numeros.Find(id);
            db.Numeros.Remove(numero);
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
