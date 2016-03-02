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
    public class CasasController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Casas
        public ActionResult Index()
        {
            return View(db.Casas.ToList());
        }

        // GET: Casas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casa casa = db.Casas.Find(id);
            if (casa == null)
            {
                return HttpNotFound();
            }
            return View(casa);
        }

        // GET: Casas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Casas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCasa,Nombre,MontoInicial,MontoAcumulado")] Casa casa)
        {
            if (ModelState.IsValid)
            {
                db.Casas.Add(casa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(casa);
        }

        // GET: Casas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casa casa = db.Casas.Find(id);
            if (casa == null)
            {
                return HttpNotFound();
            }
            return View(casa);
        }

        // POST: Casas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCasa,Nombre,MontoInicial,MontoAcumulado")] Casa casa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(casa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(casa);
        }

        // GET: Casas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Casa casa = db.Casas.Find(id);
            if (casa == null)
            {
                return HttpNotFound();
            }
            return View(casa);
        }

        // POST: Casas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Casa casa = db.Casas.Find(id);
            db.Casas.Remove(casa);
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
