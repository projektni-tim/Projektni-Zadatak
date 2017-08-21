using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ProjektniZadatak.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace ProjektniZadatak.Controllers
{
    public class UpravljanjeNalogomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: UpravljanjeNalogom/Details/5
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
            var pravoPristupa = userManager.GetRoles(id);

            ViewBag.PravoPristupa = pravoPristupa[0].ToString();

            return View(applicationUser);
        }

 

        // GET: UpravljanjeNalogom/Edit/5
        [Authorize]
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
            return View(applicationUser);
        }

        // POST: UpravljanjeNalogom/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit([Bind(Include = "Id,Ime,Prezime,Pol,Fotografija,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser, HttpPostedFileBase fotografijaIzmena)
        {
            if (ModelState.IsValid)
            {

                //Azuriranje fotografije, ukoliko je dodata nova
                if (fotografijaIzmena != null && fotografijaIzmena.ContentLength > 0)
                {

                    using (var reader = new System.IO.BinaryReader(fotografijaIzmena.InputStream))
                    {
                        applicationUser.Fotografija = reader.ReadBytes(fotografijaIzmena.ContentLength);
                    }
                }

                db.Entry(applicationUser).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.Poruka = "Uspesno ste izvrsili izmenu podataka";

                return View(applicationUser);
            }
            return View(applicationUser);
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
