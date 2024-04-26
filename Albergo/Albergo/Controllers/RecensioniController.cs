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
    public class RecensioniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Recensioni
        public ActionResult Index()
        {
            // Mostra solo le recensioni approvate
            var recensioniApprovate = db.Recensioni.Where(r => r.Approvata == true).ToList();
            return View(recensioniApprovate);
        }

        // GET: Recensioni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Recensioni/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdReview,Nome,Cognome,Voto,TestoRecensione,Approvata")] Recensioni recensioni)
        {
            if (ModelState.IsValid)
            {
                // Nuove recensioni sono impostate come non approvate di default
                recensioni.Approvata = false;

                db.Recensioni.Add(recensioni);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }

            return View(recensioni);
        }

        // GET: Recensioni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recensioni recensioni = db.Recensioni.Find(id);
            if (recensioni == null)
            {
                return HttpNotFound();
            }
            return View(recensioni);
        }

        // POST: Recensioni/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdReview,Nome,Cognome,Voto,TestoRecensione,Approvata")] Recensioni recensioni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recensioni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Pending");
            }
            return View(recensioni);
        }

        // GET: Recensioni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recensioni recensioni = db.Recensioni.Find(id);
            if (recensioni == null)
            {
                return HttpNotFound();
            }
            return View(recensioni);
        }

        // POST: Recensioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Recensioni recensioni = db.Recensioni.Find(id);
            db.Recensioni.Remove(recensioni);
            db.SaveChanges();
            return RedirectToAction("Pending");
        }

        // GET: Recensioni/Pending
        [Authorize(Roles = "Admin")] // Accessibile solo all'admin
        public ActionResult Pending()
        {
            // Mostra solo le recensioni non approvate
            var recensioniNonApprovate = db.Recensioni.Where(r => r.Approvata == false).ToList();
            return View(recensioniNonApprovate);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")] // Accessibile solo all'admin
        [ValidateAntiForgeryToken]
        public ActionResult Approve(int id)
        {
            // Verifica se l'ID fornito è valido
            if (id <= 0)
            {
                System.Diagnostics.Debug.WriteLine("ID non valido: " + id); // Debug dell'ID non valido
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest); // Errore HTTP 400 Bad Request se l'ID non è valido
            }

            // Trova la recensione corrispondente all'ID
            Recensioni recensioni = db.Recensioni.Find(id);
            if (recensioni == null)
            {
                System.Diagnostics.Debug.WriteLine("Recensione non trovata per l'ID: " + id); // Debug se la recensione non viene trovata
                return HttpNotFound(); // Errore HTTP 404 Not Found se la recensione non viene trovata
            }

            // Approva la recensione
            recensioni.Approvata = true;

            try
            {
                db.SaveChanges(); // Salva le modifiche nel database
                System.Diagnostics.Debug.WriteLine("Recensione approvata con successo. ID: " + id); // Debug se l'approvazione ha avuto successo
                return RedirectToAction("Pending"); // Reindirizza alla lista delle recensioni non approvate
            }
            catch (Exception ex)
            {
                // Gestione degli errori
                System.Diagnostics.Debug.WriteLine("Errore durante il salvataggio delle modifiche: " + ex.Message); // Debug dell'errore di salvataggio
                ModelState.AddModelError("", "Si è verificato un errore durante l'approvazione della recensione: " + ex.Message);
                return View("Error"); // Visualizza una vista di errore
            }
        }




        // Altri metodi del controller...

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
