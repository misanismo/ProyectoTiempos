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
    public class PremiosController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Premios
        public ActionResult Index()
        {
            return View(db.Premios.ToList());
        }

        // GET: Premios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // GET: Premios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Premios/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Multiveces")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Premios.Add(premio);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(premio);
        }

        // GET: Premios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // POST: Premios/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Multiveces")] Premio premio)
        {
            if (ModelState.IsValid)
            {
                db.Entry(premio).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(premio);
        }

        // GET: Premios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Premio premio = db.Premios.Find(id);
            if (premio == null)
            {
                return HttpNotFound();
            }
            return View(premio);
        }

        // POST: Premios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Premio premio = db.Premios.Find(id);
            db.Premios.Remove(premio);
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
