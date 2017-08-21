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
    public class AdresaController : Controller
    {
        private ProjektniZadatakContext db = new ProjektniZadatakContext();

        // GET: Adresa

        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Index(int id)
        {
            var adresa = db.Adresa.Include(a => a.Grad).Include(a => a.Osoba).Include(a => a.TipAdrese).Where(a => a.OsobaId == id).Select(a => a);
            ViewBag.OsobaId = id;
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(adresa.ToList());
        }



        // GET: Adresa/Create
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create(int id)
        {
            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada");
            List<TipAdrese> listaTipovaAdresa = db.TipAdrese.ToList();
            
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.TipAdrese = listaTipovaAdresa;
            ViewBag.OsobaId = id;
            return View();
        }

        // POST: Adresa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create([Bind(Include = "AdresaId,NazivAdrese,OsobaId,TipAdreseId,GradId")] Adresa adresa)
        {
            if (ModelState.IsValid)
            {
                db.Adresa.Add(adresa);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = adresa.OsobaId });
            }

            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada", adresa.GradId);
            ViewBag.TipAdreseId = new SelectList(db.TipAdrese, "TipAdreseId", "VrstaAdrese", adresa.TipAdreseId);
            var osoba = db.Osoba.Find(adresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = adresa.OsobaId;
            return View(adresa);
        }

        // GET: Adresa/Edit/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresa adresa = db.Adresa.Find(id);
            if (adresa == null)
            {
                return HttpNotFound();
            }
            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada", adresa.GradId);
            ViewBag.TipAdreseId = new SelectList(db.TipAdrese, "TipAdreseId", "VrstaAdrese", adresa.TipAdreseId);
            ViewBag.OsobaId = adresa.OsobaId;

            var osoba = db.Osoba.Find(adresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(adresa);
        }

        // POST: Adresa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit([Bind(Include = "AdresaId,NazivAdrese,OsobaId,TipAdreseId,GradId")] Adresa adresa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(adresa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = adresa.OsobaId });
            }
            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada", adresa.GradId);
            ViewBag.OsobaId = adresa.OsobaId;
            ViewBag.TipAdreseId = new SelectList(db.TipAdrese, "TipAdreseId", "VrstaAdrese", adresa.TipAdreseId);

            var osoba = db.Osoba.Find(adresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(adresa);
        }

        // GET: Adresa/Delete/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Adresa adresa = db.Adresa.Find(id);
            if (adresa == null)
            {
                return HttpNotFound();
            }

            var osoba = db.Osoba.Find(adresa.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = adresa.OsobaId;

            return View(adresa);
        }



        // POST: Adresa/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult DeleteConfirmed(int id)
        {
            Adresa adresa = db.Adresa.Find(id);
            int OsobaId = adresa.OsobaId;
            db.Adresa.Remove(adresa);
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
