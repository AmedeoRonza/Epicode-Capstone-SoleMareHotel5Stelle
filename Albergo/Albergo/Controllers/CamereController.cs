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
    public class CamereController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Camere
        public ActionResult Index()
        {
            return View(db.Camere.ToList());
        }

        // GET: Camere/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camere camere = db.Camere.Find(id);
            if (camere == null)
            {
                return HttpNotFound();
            }
            return View(camere);
        }

        // GET: Camere/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Camere/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdCamera,Piano,NumeroCamera,Tipo,Descrizione,Prezzo")] Camere camere)
        {
            if (ModelState.IsValid)
            {
                db.Camere.Add(camere);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(camere);
        }

        // GET: Camere/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camere camere = db.Camere.Find(id);
            if (camere == null)
            {
                return HttpNotFound();
            }
            return View(camere);
        }

        // POST: Camere/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdCamera,Piano,NumeroCamera,Tipo,Descrizione,Prezzo")] Camere camere)
        {
            if (ModelState.IsValid)
            {
                db.Entry(camere).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(camere);
        }

        // GET: Camere/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Camere camere = db.Camere.Find(id);
            if (camere == null)
            {
                return HttpNotFound();
            }
            return View(camere);
        }

        // POST: Camere/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Camere camere = db.Camere.Find(id);
            db.Camere.Remove(camere);
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
