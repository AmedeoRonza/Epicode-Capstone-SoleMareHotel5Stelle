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
    public class PrenotazioniServiziController : Controller
    {
        private ModelDbContext db = new ModelDbContext();

        // GET: PrenotazioniServizi
        public ActionResult Index(int? idPrenotazione)
        {
            // Filtra i servizi per l'ID della prenotazione
            var prenotazioniServizi = db.PrenotazioniServizi.Include(p => p.Prenotazioni).Include(p => p.Servizi)
                                                .Where(p => p.IdPrenotazione == idPrenotazione)
                                                .ToList();

            return View(prenotazioniServizi);
        }

        // GET: PrenotazioniServizi/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrenotazioniServizi prenotazioniServizi = db.PrenotazioniServizi.Find(id);
            if (prenotazioniServizi == null)
            {
                return HttpNotFound();
            }
            return View(prenotazioniServizi);
        }

        // GET: PrenotazioniServizi/Create
        public ActionResult Create(int idPrenotazione)
        {
            // Assicurati di avere il riferimento alla prenotazione corretta
            var prenotazione = db.Prenotazioni.Find(idPrenotazione);
            if (prenotazione == null)
            {
                // Gestisci il caso in cui non viene trovata la prenotazione corrispondente
                return HttpNotFound();
            }

            // Imposta l'ID della prenotazione nel ViewBag per utilizzarlo nella vista
            ViewBag.IdPrenotazione = idPrenotazione;

            // Passa anche l'ID della prenotazione come parametro per la creazione del servizio
            ViewBag.IdServizio = new SelectList(db.Servizi, "IdServizio", "Descrizione");

            return View();
        }

        // POST: PrenotazioniServizi/Create
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdChiave,IdPrenotazione,IdServizio,DataInizioServizio,DataFineServizio,Prezzo")] PrenotazioniServizi prenotazioniServizi)
        {
            if (ModelState.IsValid)
            {
                // Calcola la durata del servizio in giorni
                int durataServizio;
                if (prenotazioniServizi.DataInizioServizio.Date == prenotazioniServizi.DataFineServizio.Date)
                {
                    durataServizio = 1; // Se la data di inizio è uguale a quella di fine, la durata è di un giorno
                }
                else
                {
                    durataServizio = (int)(prenotazioniServizi.DataFineServizio - prenotazioniServizi.DataInizioServizio).TotalDays + 1;
                }

                // Recupera il prezzo del servizio dal database
                Servizi servizio = db.Servizi.Find(prenotazioniServizi.IdServizio);
                decimal prezzoServizio = servizio.Prezzo;

                // Calcola il prezzo totale del servizio
                prenotazioniServizi.Prezzo = prezzoServizio * durataServizio;

                // Aggiungi il servizio alla tabella PrenotazioniServizi
                db.PrenotazioniServizi.Add(prenotazioniServizi);
                db.SaveChanges();
                return RedirectToAction("Index", "Prenotazioni");
            }

            ViewBag.IdPrenotazione = new SelectList(db.Prenotazioni, "IdPrenotazione", "Nome", prenotazioniServizi.IdPrenotazione);
            ViewBag.IdServizio = new SelectList(db.Servizi, "IdServizio", "Descrizione", prenotazioniServizi.IdServizio);
            return View(prenotazioniServizi);
        }

        // GET: PrenotazioniServizi/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrenotazioniServizi prenotazioniServizi = db.PrenotazioniServizi.Find(id);
            if (prenotazioniServizi == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdPrenotazione = new SelectList(db.Prenotazioni, "IdPrenotazione", "Nome", prenotazioniServizi.IdPrenotazione);
            ViewBag.IdServizio = new SelectList(db.Servizi, "IdServizio", "Descrizione", prenotazioniServizi.IdServizio);
            return View(prenotazioniServizi);
        }

        // POST: PrenotazioniServizi/Edit/5
        // Per la protezione da attacchi di overposting, abilitare le proprietà a cui eseguire il binding. 
        // Per altri dettagli, vedere https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdChiave,IdPrenotazione,IdServizio,DataInizioServizio,DataFineServizio,Prezzo")] PrenotazioniServizi prenotazioniServizi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(prenotazioniServizi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Prenotazioni");
            }
            ViewBag.IdPrenotazione = new SelectList(db.Prenotazioni, "IdPrenotazione", "Nome", prenotazioniServizi.IdPrenotazione);
            ViewBag.IdServizio = new SelectList(db.Servizi, "IdServizio", "Descrizione", prenotazioniServizi.IdServizio);
            return View(prenotazioniServizi);
        }

        // GET: PrenotazioniServizi/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrenotazioniServizi prenotazioniServizi = db.PrenotazioniServizi.Find(id);
            if (prenotazioniServizi == null)
            {
                return HttpNotFound();
            }
            return View(prenotazioniServizi);
        }

        // POST: PrenotazioniServizi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrenotazioniServizi prenotazioniServizi = db.PrenotazioniServizi.Find(id);
            db.PrenotazioniServizi.Remove(prenotazioniServizi);
            db.SaveChanges();
            return RedirectToAction("Index", "Prenotazioni");
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
