using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektniZadatak.Models;
using ProjektniZadatak.Repo;

namespace ProjektniZadatak.Controllers
{
    public class OsobaController : Controller
    {
        private ProjektniZadatakContext db = new ProjektniZadatakContext();

        IRepository<Osoba> repo;
        IRepository<Adresa> repAdresa;
        IRepository<EmailAdresa> repEmail;
        IRepository<MobilniTelefon> repMobilni;
        IRepository<FiksniTelefon> repFiksni;
        IRepository<OsobaViewModel> repOVM;

        public OsobaController()
        {
            repo = new Repository<Osoba>(db);
            repAdresa = new Repository<Adresa>(db);
            repEmail = new Repository<EmailAdresa>(db);
            repMobilni = new Repository<MobilniTelefon>(db);
            repFiksni = new Repository<FiksniTelefon>(db);
            repOVM = new Repository<OsobaViewModel>(db);
        }

        public OsobaController(IRepository<Osoba> r)
        {
            repo = r;
        }

        public OsobaController(IRepository<OsobaViewModel> r)
        {
            repOVM = r;
        }
        public OsobaController(IRepository<Osoba> r, IRepository<Adresa> a,
            IRepository<EmailAdresa> e, IRepository<MobilniTelefon> m,
            IRepository<FiksniTelefon> f)
        {
            repo = r;
            repAdresa = a;
            repEmail = e;
            repMobilni = m;
            repFiksni = f;
        }

        public OsobaController(IRepository<Osoba> r, IRepository<Adresa> a,
            IRepository<EmailAdresa> e, IRepository<MobilniTelefon> m,
            IRepository<FiksniTelefon> f, IRepository<OsobaViewModel> o)
        {
            repo = r;
            repAdresa = a;
            repEmail = e;
            repMobilni = m;
            repFiksni = f;
            repOVM = o;
        }
        
        public JsonResult VratiOpstine(int? id)
        {
            IQueryable<OpstinaViewModel> listaOpstina = db.Opstina
                               .Select(o=>new OpstinaViewModel {
                               OpstinaId=o.OpstinaId,
                               NazivOpstine=o.NazivOpstine,
                               GradId=o.GradId});

            if(id!=null)
            {
                listaOpstina = listaOpstina
                            .Where(o => o.GradId == id)
                            .Select(o => o);
            }

            return Json(listaOpstina.ToList(), JsonRequestBehavior.AllowGet);
        }


        // GET: Osoba
        [Authorize]
        public ActionResult Index()
        {
            //var osoba = db.Osoba.Include(o => o.Opstina).Include(o => o.Pol);
            //return View(osoba.ToList());
            return View(repo.GetAll());
        }

        // GET: Osoba/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Osoba osoba = db.Osoba.Find(id);
            int Oid = (int)id;
            var osoba = repo.GetAll().Where(i => i.OsobaId == Oid).FirstOrDefault();

            if (osoba == null)
            {
                return HttpNotFound();
            }

            //ViewBag.Adrese = db.Adresa.Where(a => a.OsobaId == id).Select(a => a).ToList();
            //ViewBag.MobilniTelefoni = db.MobilniTelefon.Where(m => m.OsobaId == id).Select(m => m).ToList();
            //ViewBag.FiksniTelefoni = db.FiksniTelefon.Where(f => f.OsobaId == id).Select(f => f).ToList();
            //ViewBag.EmailAdrese = db.EmailAdresa.Where(e => e.OsobaId == id).Select(e => e).ToList();
            ViewBag.Adrese = repAdresa.GetAll().Where(a => a.OsobaId == id).Select(a => a).ToList();
            ViewBag.MobilniTelefoni = repMobilni.GetAll().Where(m => m.OsobaId == id).Select(m => m).ToList();
            ViewBag.FiksniTelefoni = repFiksni.GetAll().Where(f => f.OsobaId == id).Select(f => f).ToList();
            ViewBag.EmailAdrese = repEmail.GetAll().Where(e => e.OsobaId == id).Select(e => e).ToList();

            return View(osoba);
        }

        // GET: Osoba/Create
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Create()
        {

            //Osoba 
            ViewBag.GradRodjenja =db.Grad.ToList();
            ViewBag.PolId = new SelectList(db.Pol, "PolId", "NazivPola");


            //Adresa
            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada");
            ViewBag.TipAdreseId = new SelectList(db.TipAdrese, "TipAdreseId", "VrstaAdrese");

            //Email adresa
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese");

            //Mobilni telefon
            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob");
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni");

            //Fiksni telefon
            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik");
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni");



            return View();
        }

        // POST: Osoba/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Create(OsobaViewModel model, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                //Dodavanje osobe
                var osoba = new Osoba
                {
                    Ime = model.Ime,
                    Prezime = model.Prezime,
                    ImeRoditelja = model.ImeRoditelja,
                    JMBG = model.JMBG,
                    DatumRodjenja = model.DatumRodjenja,
                    BrojLicneKarte = model.BrojLicneKarte,
                    PolId = model.PolId,
                    OpstinaRodjenjaId = model.OpstinaRodjenjaId,
                    Beleska = model.Beleska
                };

                //Dodavanje fotografije za osobu
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        osoba.Fotografija = reader.ReadBytes(upload.ContentLength);
                    }

                }

                //db.Osoba.Add(osoba);
                repo.Add(osoba);

                db.SaveChanges();

                //Dodavanje adrese
                if (!String.IsNullOrEmpty(model.NazivAdrese))
                {
                    var adresa = new Adresa
                    {
                        OsobaId = osoba.OsobaId,
                        TipAdreseId = model.TipAdreseId,
                        GradId = model.GradId,
                        NazivAdrese = model.NazivAdrese
                    };

                    //db.Adresa.Add(adresa);
                    repAdresa.Add(adresa);
                    db.SaveChanges();
                }

                //Dodavanje email adrese
                if (!String.IsNullOrEmpty(model.NazivEmailAdrese))
                {

                    var emailAdresa = new EmailAdresa
                    {
                        OsobaId = osoba.OsobaId,
                        TipEmailAdreseId = model.TipEmailAdreseId,
                        NazivEmailAdrese = model.NazivEmailAdrese
                    };

                    //db.EmailAdresa.Add(emailAdresa);
                    repEmail.Add(emailAdresa);
                    db.SaveChanges();
                }

                //Dodavanje mobilnog telefona
                if (!String.IsNullOrEmpty(model.BrojMobilnog))
                {

                    var mobilniTelefon = new MobilniTelefon
                    {
                        OsobaId = osoba.OsobaId,
                        TipMobilniId = model.TipMobilniId,
                        LokalMobilniId = model.LokalMobilniId,
                        BrojMobilnog = model.BrojMobilnog
                    };

                    //db.MobilniTelefon.Add(mobilniTelefon);
                    repMobilni.Add(mobilniTelefon);
                    db.SaveChanges();
                }

                //Dodavanje fiksnog telefona
                if (!String.IsNullOrEmpty(model.BrojFiksnog))
                {

                    var fiksniTelefon = new FiksniTelefon
                    {
                        OsobaId = osoba.OsobaId,
                        TipFiksniId = model.TipFiksniId,
                        LokalFiksniId = model.LokalFiksniId,
                        BrojFiksnog = model.BrojFiksnog
                    };

                    //db.FiksniTelefon.Add(fiksniTelefon);
                    repFiksni.Add(fiksniTelefon);
                    db.SaveChanges();
                }


                return Redirect("Index");

            }
            //Osoba
            ViewBag.GradRodjenjaId = new SelectList(db.Grad, "GradId", "NazivGrada");
            ViewBag.PolId = new SelectList(db.Pol, "PolId", "NazivPola", model.PolId);

            //Adresa
            ViewBag.GradId = new SelectList(db.Grad, "GradId", "NazivGrada", model.GradId);
            ViewBag.TipAdrese = db.TipAdrese.ToList();

            //Email adresa
            ViewBag.TipEmailAdreseId = new SelectList(db.TipEmailAdrese, "TipEmailAdreseId", "VrstaEmailAdrese", model.TipEmailAdreseId);

            //Mobilni telefon
            ViewBag.LokalMobilniId = new SelectList(db.LokalMobilni, "LokalMobilniId", "LokalMob", model.LokalMobilniId);
            ViewBag.TipMobilniId = new SelectList(db.TipMobilni, "TipMobilniId", "VrstaMobilni", model.TipMobilniId);

            //Fiksni telefon
            ViewBag.LokalFiksniId = new SelectList(db.LokalFiksni, "LokalFiksniId", "LokalFik", model.LokalFiksniId);
            ViewBag.TipFiksniId = new SelectList(db.TipFiskni, "TipFiksniId", "VrstaFiksni", model.TipFiksniId);

            return View(model);
        }

        // GET: Osoba/Edit/5
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Osoba osoba = db.Osoba.Find(id);
            if (osoba == null)
            {
                return HttpNotFound();
            }

            ViewBag.OpstinaRodjenjaId = new SelectList(db.Opstina, "OpstinaId", "NazivOpstine", osoba.OpstinaRodjenjaId);
            ViewBag.GradRodjenjaId = new SelectList(db.Grad, "GradId", "NazivGrada", osoba.Opstina.GradId);
            ViewBag.PolId = new SelectList(db.Pol, "PolId", "NazivPola", osoba.PolId);
            return View(osoba);
        }

        // POST: Osoba/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije, Pravo unosa")]
        public ActionResult Edit([Bind(Include = "OsobaId,Ime,Prezime,ImeRoditelja,JMBG,BrojLicneKarte,DatumRodjenja,OpstinaRodjenjaId,PolId,Fotografija,Beleska")] Osoba osoba, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {

                //Izmena fotografije korisnika, ukoliko je dodata nova
                if (upload != null && upload.ContentLength > 0)
                {
                    using (var reader = new System.IO.BinaryReader(upload.InputStream))
                    {
                        osoba.Fotografija = reader.ReadBytes(upload.ContentLength);
                    }

                }

                //db.Entry(osoba).State = EntityState.Modified;
                repo.Edit(osoba);

                db.SaveChanges();
                return RedirectToAction("Index");

            }
            ViewBag.OpstinaRodjenjaId = new SelectList(db.Opstina, "OpstinaId", "NazivOpstine", osoba.OpstinaRodjenjaId);
            ViewBag.PolId = new SelectList(db.Pol, "PolId", "NazivPola", osoba.PolId);
            return View(osoba);
        }

        // GET: Osoba/Delete/5
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Osoba osoba = db.Osoba.Find(id);
            int Oid = (int)id;
            Osoba osoba = repo.GetAll().Where(i => i.OsobaId == Oid).FirstOrDefault();

            if (osoba == null)
            {
                return HttpNotFound();
            }
            return View(osoba);
        }

        // POST: Osoba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult DeleteConfirmed(int id)
        {
            //Brisanje adresa 
            //List<Adresa> listaAdresa = db.Adresa.Where(a => a.OsobaId == id).Select(a => a).ToList();
            List<Adresa> listaAdresa = repAdresa.GetAll().Where(a => a.OsobaId == id).Select(a => a).ToList();

            if (listaAdresa != null)
            {
                foreach (var adresa in listaAdresa)
                {
                    //db.Adresa.Remove(adresa);
                    repAdresa.Remove(adresa);
                    db.SaveChanges();
                }
            }



            //Brisanje mobilnih telefona
            //List<MobilniTelefon> listaMobilnihTelefona = db.MobilniTelefon.Where(m => m.OsobaId == id).Select(m => m).ToList();
            List<MobilniTelefon> listaMobilnihTelefona = repMobilni.GetAll().Where(m => m.OsobaId == id).Select(m => m).ToList();

            if (listaMobilnihTelefona != null)
            {
                foreach (var mobilniTelefon in listaMobilnihTelefona)
                {
                    //db.MobilniTelefon.Remove(mobilniTelefon);
                    repMobilni.Remove(mobilniTelefon);
                    db.SaveChanges();
                }
            }

            //Brisanje fiksnih telefona
            //List<FiksniTelefon> listaFiksnihTelefona = db.FiksniTelefon.Where(f => f.OsobaId == id).Select(f => f).ToList();
            List<FiksniTelefon> listaFiksnihTelefona = repFiksni.GetAll().Where(f => f.OsobaId == id).Select(f => f).ToList();

            if (listaFiksnihTelefona != null)
            {
                foreach (var fiksniTelefon in listaFiksnihTelefona)
                {
                    //db.FiksniTelefon.Remove(fiksniTelefon);
                    repFiksni.Remove(fiksniTelefon);
                    db.SaveChanges();
                }
            }


            //Brisanje email adresa
            //List<EmailAdresa> listaEmailAdresa = db.EmailAdresa.Where(e => e.OsobaId == id).Select(e => e).ToList();
            List<EmailAdresa> listaEmailAdresa = repEmail.GetAll().Where(e => e.OsobaId == id).Select(e => e).ToList();

            if (listaEmailAdresa != null)
            {
                foreach (var emailAdresa in listaEmailAdresa)
                {
                    //db.EmailAdresa.Remove(emailAdresa);
                    repEmail.Remove(emailAdresa);
                    db.SaveChanges();
                }
            }



            //Osoba osoba = db.Osoba.Find(id);
            //db.Osoba.Remove(osoba);
            Osoba osoba = repo.GetAll().Where(o => o.OsobaId == id).SingleOrDefault();
            repo.Remove(osoba);

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
