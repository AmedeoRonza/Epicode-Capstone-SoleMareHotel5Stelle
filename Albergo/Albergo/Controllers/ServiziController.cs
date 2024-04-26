using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Albergo.Models;

namespace Albergo.Controllers
{
    public class ServiziController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Servizi
        public ActionResult Index()
        {
            return View(db.Servizi.ToList());
        }

        // GET: Servizi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizi.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // GET: Servizi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Servizi/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdServizio,Descrizione,Prezzo")] Servizi servizi)
        {
            if (ModelState.IsValid)
            {
                db.Servizi.Add(servizi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(servizi);
        }

        // GET: Servizi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizi.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizi/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdServizio,Descrizione,Prezzo")] Servizi servizi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(servizi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(servizi);
        }

        // GET: Servizi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Servizi servizi = db.Servizi.Find(id);
            if (servizi == null)
            {
                return HttpNotFound();
            }
            return View(servizi);
        }

        // POST: Servizi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Servizi servizi = db.Servizi.Find(id);
            db.Servizi.Remove(servizi);
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
