using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektniZadatak.Models;

namespace ProjektniZadatak.Controllers
{
    public class EmailAdresaController : Controller
    {
        private ProjektniZadatakContext db = new ProjektniZadatakContext();

        // GET: EmailAdresa
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Index(int id)
        {
            var emailAdresa = db.EmailAdresa.Include(e => e.Osoba).Include(e => e.TipEmailAdrese).Where(e => e.OsobaId == id);
            ViewBag.OsobaId = id;
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime+" "+osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(emailAdresa.ToList());
        }



        // GET: EmailAdresa/Create
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create(int id)
        {
            ViewBag.OsobaId = id;
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese");
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View();
        }

        // POST: EmailAdresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create([Bind(Include = "EmailAdresaId,NazivEmailAdrese,OsobaId,TipEmailAdreseId")] EmailAdresa emailAdresa)
        {
            if (ModelState.IsValid)
            {
                db.EmailAdresa.Add(emailAdresa);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = emailAdresa.OsobaId });
            }


            var osoba = db.Osoba.Find(emailAdresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = emailAdresa.OsobaId;
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese", emailAdresa.TipEmailAdreseId);
            return View(emailAdresa);
        }

        // GET: EmailAdresa/Edit/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAdresa emailAdresa = db.EmailAdresa.Find(id);
            if (emailAdresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.OsobaId = emailAdresa.OsobaId;
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese", emailAdresa.TipEmailAdreseId);
            var osoba = db.Osoba.Find(emailAdresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(emailAdresa);
        }

        // POST: EmailAdresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit([Bind(Include = "EmailAdresaId,NazivEmailAdrese,OsobaId,TipEmailAdreseId")] EmailAdresa emailAdresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(emailAdresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = emailAdresa.OsobaId });
            }
            ViewBag.OsobaId = emailAdresa.OsobaId;
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese", emailAdresa.TipEmailAdreseId);
            var osoba = db.Osoba.Find(emailAdresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(emailAdresa);
        }

        // GET: EmailAdresa/Delete/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmailAdresa emailAdresa = db.EmailAdresa.Find(id);
            if (emailAdresa == null)
            {
                return HttpNotFound();
            }
            var osoba = db.Osoba.Find(emailAdresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = emailAdresa.OsobaId;
            return View(emailAdresa);
        }

        // POST: EmailAdresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult DeleteConfirmed(int id)
        {
            EmailAdresa emailAdresa = db.EmailAdresa.Find(id);
            int OsobaId = emailAdresa.OsobaId;
            db.EmailAdresa.Remove(emailAdresa);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = OsobaId });
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
