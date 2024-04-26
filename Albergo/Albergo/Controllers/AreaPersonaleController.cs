using Albergo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Albergo.Controllers
{
    public class AreapersonaleController : Controller
    {
        private ModelDbContext db = new ModelDbContext();
        // GET: Areapersonale
        public ActionResult Index()
        {
            // Verifica se l'utente è autenticato
            if (User.Identity.IsAuthenticated)
            {
                // Recupera l'email dell'utente corrente
                string userEmail = User.Identity.Name;

                // Query per recuperare le prenotazioni dell'utente corrente utilizzando l'email
                var prenotazioniUtente = db.Prenotazioni.Where(p => p.Email == userEmail).ToList();

                // Passa le prenotazioni alla vista
                return View(prenotazioniUtente);
            }
            else
            {
                // Se l'utente non è autenticato, reindirizzalo alla pagina di login
                return RedirectToAction("Login", "Account");
            }
        }
    }
}