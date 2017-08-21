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
    public class MobilniTelefonController : Controller
    {
        private ProjektniZadatakContext db = new ProjektniZadatakContext();

        // GET: MobilniTelefon
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Index(int id)
        {
            var mobilniTelefon = db.MobilniTelefon.Include(m => m.LokalMobilni).Include(m => m.Osoba).Include(m => m.TipMobilni).Where(m => m.OsobaId == id).Select(m => m).ToList();
            ViewBag.OsobaId = id;
            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(mobilniTelefon.ToList());
        }



        // GET: MobilniTelefon/Create
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create(int id)
        {
            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob");
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni");
            ViewBag.OsobaId = id;

            var osoba = db.Osoba.Find(id);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View();
        }

        // POST: MobilniTelefon/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Create([Bind(Include = "MobilniTelefonId,LokalMobilniId,TipMobilniId,BrojMobilnog,OsobaId")] MobilniTelefon mobilniTelefon)
        {
            if (ModelState.IsValid)
            {
                db.MobilniTelefon.Add(mobilniTelefon);
                db.SaveChanges();
                return RedirectToAction("Index", new { id = mobilniTelefon.OsobaId });

            }

            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob", mobilniTelefon.LokalMobilniId);
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni", mobilniTelefon.TipMobilniId);
            ViewBag.OsobaId = mobilniTelefon.OsobaId;
            var osoba = db.Osoba.Find(mobilniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(mobilniTelefon);
        }

        // GET: MobilniTelefon/Edit/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobilniTelefon mobilniTelefon = db.MobilniTelefon.Find(id);
            if (mobilniTelefon == null)
            {
                return HttpNotFound();
            }
            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob", mobilniTelefon.LokalMobilniId);
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni", mobilniTelefon.TipMobilniId);
            ViewBag.OsobaId = mobilniTelefon.OsobaId;
            var osoba = db.Osoba.Find(mobilniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;

            return View(mobilniTelefon);
        }

        // POST: MobilniTelefon/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit([Bind(Include = "MobilniTelefonId,LokalMobilniId,TipMobilniId,BrojMobilnog,OsobaId")] MobilniTelefon mobilniTelefon)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mobilniTelefon).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", new { id = mobilniTelefon.OsobaId });
            }
            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob", mobilniTelefon.LokalMobilniId);
            ViewBag.OsobaId = mobilniTelefon.OsobaId;
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni", mobilniTelefon.TipMobilniId);
            var osoba = db.Osoba.Find(mobilniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            return View(mobilniTelefon);
        }

        // GET: MobilniTelefon/Delete/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MobilniTelefon mobilniTelefon = db.MobilniTelefon.Find(id);
            if (mobilniTelefon == null)
            {
                return HttpNotFound();
            }
            var osoba = db.Osoba.Find(mobilniTelefon.OsobaId);
            ViewBag.ImePrezime = osoba.Ime + " " + osoba.Prezime;
            ViewBag.Fotografija = osoba.Fotografija;
            ViewBag.OsobaId = mobilniTelefon.OsobaId;
            return View(mobilniTelefon);
        }

        // POST: MobilniTelefon/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult DeleteConfirmed(int id)
        {
            MobilniTelefon mobilniTelefon = db.MobilniTelefon.Find(id);
            db.MobilniTelefon.Remove(mobilniTelefon);
            db.SaveChanges();
            return RedirectToAction("Index", new { id = mobilniTelefon.OsobaId });
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
