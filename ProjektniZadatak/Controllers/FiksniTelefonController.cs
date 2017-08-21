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
    public class FiksniTelefonController : Controller
    {
        private ProjektniZadatakContext db = new ProjektniZadatakContext();

        // GET: FiksniTelefon
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Index(int id)
        {
            var fiksniTelefon = db.FiksniTelefon.Include(f => f.LokalFiksni).Include(f => f.Osoba).Include(f => f.TipFiskni).Where(f => f.OsobaId == id);
            ViewBag.OsobaId = id;
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(fiksniTelefon.ToList());
        }



        // GET: FiksniTelefon/Create
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create(int id)
        {
            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik");
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni");
            ViewBag.OsobaId = id;
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View();
        }

        // POST: FiksniTelefon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create([Bind(Include = "FiksniTelefonId,LokalFiksniId,TipFiksniId,BrojFiksnog,OsobaId")] FiksniTelefon fiksniTelefon)
        {
            if (ModelState.IsValid)
            {
                db.FiksniTelefon.Add(fiksniTelefon);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = fiksniTelefon.OsobaId });
            }

            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik", fiksniTelefon.LokalFiksniId);
            ViewBag.OsobaId = fiksniTelefon.OsobaId;
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni", fiksniTelefon.TipFiksniId);
            var osoba = db.Osoba.Find(fiksniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(fiksniTelefon);
        }

        // GET: FiksniTelefon/Edit/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiksniTelefon fiksniTelefon = db.FiksniTelefon.Find(id);
            if (fiksniTelefon == null)
            {
                return HttpNotFound();
            }
            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik", fiksniTelefon.LokalFiksniId);
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni", fiksniTelefon.TipFiksniId);
            ViewBag.OsobaId = fiksniTelefon.OsobaId;
            var osoba = db.Osoba.Find(fiksniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(fiksniTelefon);
        }

        // POST: FiksniTelefon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit([Bind(Include = "FiksniTelefonId,LokalFiksniId,TipFiksniId,BrojFiksnog,OsobaId")] FiksniTelefon fiksniTelefon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fiksniTelefon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = fiksniTelefon.OsobaId });
            }
            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik", fiksniTelefon.LokalFiksniId);
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni", fiksniTelefon.TipFiksniId);
            ViewBag.OsobaId = fiksniTelefon.OsobaId;
            var osoba = db.Osoba.Find(fiksniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(fiksniTelefon);
        }

        // GET: FiksniTelefon/Delete/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            FiksniTelefon fiksniTelefon = db.FiksniTelefon.Find(id);
            if (fiksniTelefon == null)
            {
                return HttpNotFound();
            }

            var osoba = db.Osoba.Find(fiksniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = fiksniTelefon.OsobaId;
            return View(fiksniTelefon);
        }

        // POST: FiksniTelefon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult DeleteConfirmed(int id)
        {
            FiksniTelefon fiksniTelefon = db.FiksniTelefon.Find(id);
            db.FiksniTelefon.Remove(fiksniTelefon);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = fiksniTelefon.OsobaId });
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
