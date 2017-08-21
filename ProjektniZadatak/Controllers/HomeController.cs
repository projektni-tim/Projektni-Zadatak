using ProjektniZadatak.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
namespace ProjektniZadatak.Controllers
{
    public class HomeController : Controller
    {


        // GET: Osoba
        public ActionResult Index()
        {
            return View();
        }



    }
}