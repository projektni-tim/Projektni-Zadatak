using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektniZadatak.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace ProjektniZadatak.Controllers
{
    public class KorisnikController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Korisnik
        [Authorize]
        public ActionResult Index()
        {
           
            return View(db.Users.ToList());
        }

        [Authorize(Roles = "Pravo administracije")]
        public ActionResult PromenaLozinke(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }


            //Dodato zbog iscitavanja prava pristupa za konkretnog korisnika
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var pravoPristupa = userManager.GetRoles(id);

            ViewBag.PravoPristupaKorisnika = pravoPristupa[0].ToString();
            ViewBag.PravaPristupa = db.Roles.ToList();

            ViewBag.Poruka = "Uspesno ste promenili lozinku";
            return View("Edit", applicationUser);

        }

        // GET: Korisnik/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }
            //Dodato zbog iscitavanja prava pristupa za konkretnog korisnika
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var pravoPristupa= userManager.GetRoles(id);

            ViewBag.PravoPristupa = pravoPristupa[0].ToString();
            return View(applicationUser);
        }

        // GET: Korisnik/Create
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Korisnik/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Create([Bind(Include = "Id,Ime,Prezime,Pol,Fotografija,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(applicationUser);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicationUser);
        }

        // GET: Korisnik/Edit/5
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }


            //Dodato zbog iscitavanja prava pristupa za konkretnog korisnika
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var pravoPristupa = userManager.GetRoles(id);

            ViewBag.PravoPristupaKorisnika= pravoPristupa[0].ToString();
            ViewBag.PravaPristupa = db.Roles.ToList();
            return View(applicationUser);
        }



        // POST: Korisnik/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije")]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Ime,Prezime,Pol,Fotografija,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser, HttpPostedFileBase fotografijaIzmena, string Privilegija)
        {
            if (ModelState.IsValid)
            {
                 if (fotografijaIzmena != null && fotografijaIzmena.ContentLength > 0)
                 {

                     using (var reader = new System.IO.BinaryReader(fotografijaIzmena.InputStream))
                     {
                         applicationUser.Fotografija = reader.ReadBytes(fotografijaIzmena.ContentLength);
                     }
                 }

                //Dodato zbog iscitavanja prava pristupa za konkretnog korisnika
                var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var pravoPristupa = userManager.GetRoles(applicationUser.Id);

                //Azuriranje prava pristupa za konkretnog korisnika
                await userManager.RemoveFromRoleAsync(applicationUser.Id, pravoPristupa[0].ToString());
                await userManager.AddToRoleAsync(applicationUser.Id, Privilegija);

                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();

                var novoPravoPristupa = userManager.GetRoles(applicationUser.Id);

                //Iscitavanje auzuriranog prava pristupa
                ViewBag.PravoPristupaKorisnika = novoPravoPristupa[0].ToString();
                ViewBag.PravaPristupa = db.Roles.ToList();

                ViewBag.Poruka = "Uspešno ste izvršili izmenu podataka";

                return View(applicationUser);
            }
            return View(applicationUser);
        }

        // GET: Korisnik/Delete/5
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = db.Users.Find(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }


            //Dodato zbog iscitavanja prava pristupa za konkretnog korisnika
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var pravoPristupa = userManager.GetRoles(id);

            ViewBag.PravoPristupa = pravoPristupa[0].ToString();

            return View(applicationUser);
        }

        // POST: Korisnik/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Pravo administracije")]
        public ActionResult DeleteConfirmed(string id)
        {
            ApplicationUser applicationUser = db.Users.Find(id);

            if (applicationUser != null)
            {
                db.Users.Remove(applicationUser);
                db.SaveChanges();
            }
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
