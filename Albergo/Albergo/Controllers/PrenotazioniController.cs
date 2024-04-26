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
    public class PrenotazioniController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: Prenotazioni
        public ActionResult Index()
        {
            return View(db.Prenotazioni.ToList());
        }

        // GET: Prenotazioni/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenotazioni prenotazioni = db.Prenotazioni.Find(id);
            if (prenotazioni == null)
            {
                return HttpNotFound();
            }
            return View(prenotazioni);
        }

        // GET: Prenotazioni/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prenotazioni/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPrenotazione,Nome,Cognome,Citta,Email,DataCheckIn,DataCheckOut,TipoStanza,Numero,Prezzo")] Prenotazioni prenotazioni)
        {
            if (ModelState.IsValid)
            {
                // Calcola la durata del soggiorno
                var durataSoggiorno = (prenotazioni.DataCheckOut - prenotazioni.DataCheckIn).Days;

                // Determina il prezzo base in base al tipo di stanza
                decimal prezzoBase = 0;

                switch (prenotazioni.TipoStanza)
                {
                    case "Singola":
                        prezzoBase = 50.00m;
                        break;
                    case "Doppia":
                        prezzoBase = 100.00m;
                        break;
                    case "Tripla":
                        prezzoBase = 150.00m;
                        break;
                    case "Quadrupla":
                        prezzoBase = 200.00m;
                        break;
                    case "Matrimoniale":
                        prezzoBase = 80.00m;
                        break;
                    case "Familiare":
                        prezzoBase = 180.00m;
                        break;
                    case "Suite":
                        prezzoBase = 500.00m;
                        break;
                    // Aggiungi altri casi per gestire altri tipi di stanze, se necessario
                    default:
                        // Gestione nel caso in cui il tipo di stanza non sia riconosciuto
                        // Puoi impostare un prezzo base generico o gestire l'errore in altro modo
                        prezzoBase = 0; // O un prezzo predefinito
                        break;
                }

                // Calcola il prezzo totale
                prenotazioni.Prezzo = prezzoBase * durataSoggiorno;

                // Salva la prenotazione nel database
                db.Prenotazioni.Add(prenotazioni);
                db.SaveChanges();
                return RedirectToAction("Index", "Areapersonale");
            }

            return View(prenotazioni);
        }

        // GET: Prenotazioni/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenotazioni prenotazioni = db.Prenotazioni.Find(id);
            if (prenotazioni == null)
            {
                return HttpNotFound();
            }
            return View(prenotazioni);
        }

        // POST: Prenotazioni/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPrenotazione,Nome,Cognome,Citta,Email,DataCheckIn,DataCheckOut,TipoStanza,Numero,Prezzo")] Prenotazioni prenotazioni)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prenotazioni).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(prenotazioni);
        }

        // GET: Prenotazioni/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Prenotazioni prenotazioni = db.Prenotazioni.Find(id);
            if (prenotazioni == null)
            {
                return HttpNotFound();
            }
            return View(prenotazioni);
        }

        // POST: Prenotazioni/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Prenotazioni prenotazioni = db.Prenotazioni.Find(id);
            db.Prenotazioni.Remove(prenotazioni);
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
