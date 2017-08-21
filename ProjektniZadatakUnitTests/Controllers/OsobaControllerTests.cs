using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjektniZadatak.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using System.Web.Mvc;
using ProjektniZadatak.Repo;
using ProjektniZadatak.Models;

namespace ProjektniZadatak.Controllers.UnitTests
{
    [TestClass()]
    public class OsobaControllerTests
    {
        [TestMethod()]
        public void IndexTestVracaSveOsobe()
        {
            //Arange
            var osobe = VratiOsobe();

            var repo = new Mock<IRepository<Osoba>>();
            repo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var oc = new OsobaController(repo.Object);

            //Act
            var result = oc.Index() as ViewResult;
            var model = result.Model as IEnumerable<Osoba>;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(3, model.Count());
            Assert.AreEqual(model.First().Ime, "Srki");
            Assert.AreEqual(model.First(), osobe.First());
        }

        [TestMethod()]
        public void DetailsTestDobruOsobu()
        {
            //Arange
            var osobe = VratiOsobe();
            var emailAdrese = VratiEmailAdrese();
            var adrese = VratiAdrese();
            var mobilni = VratiMobilneTelefone();
            var fiksni = VratiFiksneTelefone();

            var repo = new Mock<IRepository<Osoba>>();
            repo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var repA = new Mock<IRepository<Adresa>>();
            repA.Setup(e => e.GetAll()).Returns(adrese.AsQueryable());
            var repE = new Mock<IRepository<EmailAdresa>>();
            repE.Setup(e => e.GetAll()).Returns(emailAdrese.AsQueryable());
            var repM = new Mock<IRepository<MobilniTelefon>>();
            repM.Setup(e => e.GetAll()).Returns(mobilni.AsQueryable());
            var repF = new Mock<IRepository<FiksniTelefon>>();
            repF.Setup(e => e.GetAll()).Returns(fiksni.AsQueryable());

            var oc = new OsobaController(repo.Object, repA.Object, repE.Object, repM.Object, repF.Object);

            //Act
            var result = oc.Details(2) as ViewResult;
            var model = result.Model as Osoba;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model.Ime, "Aca");
            Assert.AreEqual(model, osobe.Single(i => i.OsobaId == 2));
        }

        [TestMethod()]
        public void CreateTestVracaIndex()
        {
            //Arange

            var osoba = new OsobaViewModel
            {
                Ime = "Srki",
                Prezime = "Krivokuca",
                ImeRoditelja = "Marko",
                JMBG = "1231231231321",
                BrojLicneKarte = "123456789",
                PolId = 2,
                OpstinaRodjenjaId = 1,
                Beleska = "Dobar momak!"
            };
            var osobaa = new Osoba
            {
                Ime = "Srki",
                Prezime = "Krivokuca",
                ImeRoditelja = "Marko",
                JMBG = "1231231231321",
                BrojLicneKarte = "123456789",
                PolId = 2,
                OpstinaRodjenjaId = 1,
                Beleska = "Dobar momak!"
            };

            var osobe = VratiOsobe();
            var emailAdrese = VratiEmailAdrese();
            var adrese = VratiAdrese();
            var mobilni = VratiMobilneTelefone();
            var fiksni = VratiFiksneTelefone();

            var repoo = new Mock<IRepository<Osoba>>();
            repoo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var repA = new Mock<IRepository<Adresa>>();
            repA.Setup(e => e.GetAll()).Returns(adrese.AsQueryable());
            var repE = new Mock<IRepository<EmailAdresa>>();
            repE.Setup(e => e.GetAll()).Returns(emailAdrese.AsQueryable());
            var repM = new Mock<IRepository<MobilniTelefon>>();
            repM.Setup(e => e.GetAll()).Returns(mobilni.AsQueryable());
            var repF = new Mock<IRepository<FiksniTelefon>>();
            repF.Setup(e => e.GetAll()).Returns(fiksni.AsQueryable());

            var repo = new Mock<IRepository<OsobaViewModel>>();
            repo.Setup(e => e.Add(osoba));
            //var oc = new OsobaController(repo.Object);
            var oc = new OsobaController(repoo.Object, repA.Object, repE.Object, repM.Object, repF.Object, repo.Object);

            //Act
            var result = oc.Create(osoba, null);
            //Assert
            Assert.IsNotNull(result);
            //Assert.AreEqual("Index", result.GetType().Module);
            //repo.Verify(r => r.Add(osoba));
        }





        [TestMethod()]
        public void EditTestVracaNaIndex()
        {
            //Arange
            var osobe = VratiOsobe();

            var repo = new Mock<IRepository<Osoba>>();
            repo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var oc = new OsobaController(repo.Object);

            //Act
            //var result = oc.Edit(osobe.FirstOrDefault(o => o.OsobaId == 2), null) as RedirectToRouteResult;
            //var model = result.Model as Osoba;
            //Assert
            //Assert.IsNotNull(result);
            // Assert.AreEqual("Index", result.RouteValues["action"].ToString());


        }


        [TestMethod()]
        public void DeleteTest()
        {
            //Arange
            var osobe = VratiOsobe();

            var repo = new Mock<IRepository<Osoba>>();
            repo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var oc = new OsobaController(repo.Object);

            //Act
            var result = oc.Delete(1) as ViewResult;
            var model = result.Model as Osoba;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(model, osobe.Single(i => i.OsobaId == 1));
        }



        [TestMethod()]
        public void DeleteConfirmedTestVracaNaIndex()
        {
            //Arange
            var osobe = VratiOsobe();
            var emailAdrese = VratiEmailAdrese();
            var adrese = VratiAdrese();
            var mobilni = VratiMobilneTelefone();
            var fiksni = VratiFiksneTelefone();

            var repo = new Mock<IRepository<Osoba>>();
            repo.Setup(e => e.GetAll()).Returns(osobe.AsQueryable());
            var repA = new Mock<IRepository<Adresa>>();
            repA.Setup(e => e.GetAll()).Returns(adrese.AsQueryable());
            var repE = new Mock<IRepository<EmailAdresa>>();
            repE.Setup(e => e.GetAll()).Returns(emailAdrese.AsQueryable());
            var repM = new Mock<IRepository<MobilniTelefon>>();
            repM.Setup(e => e.GetAll()).Returns(mobilni.AsQueryable());
            var repF = new Mock<IRepository<FiksniTelefon>>();
            repF.Setup(e => e.GetAll()).Returns(fiksni.AsQueryable());

            var oc = new OsobaController(repo.Object, repA.Object, repE.Object, repM.Object, repF.Object);


            //Act
            var result = oc.DeleteConfirmed(1) as RedirectToRouteResult;
            //var model = result.Model as Osoba;
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.RouteValues["action"].ToString());
        }

        #region FunkcijeZaVracanjePodataka

        public IEnumerable<Osoba> VratiOsobe()
        {
            return new List<Osoba>
            {
                new Osoba
                {
                    OsobaId = 1, Ime ="Srki", Prezime="Krivokuca", ImeRoditelja="Marko",
                    JMBG ="1231231231321", BrojLicneKarte="123456789", PolId = 2 , OpstinaRodjenjaId=1, Beleska="Dobar momak!"
                },

                new Osoba
                {
                    OsobaId = 2, Ime ="Aca", Prezime="Zivkovic",
                    JMBG ="555333", BrojLicneKarte="555", PolId = 2 , OpstinaRodjenjaId=1, Beleska="Vazduplohovna :D"
                },

                new Osoba
                {
                    OsobaId = 3, Ime ="Vlada", Prezime="Janjic", ImeRoditelja="Ivan",
                    JMBG ="789", BrojLicneKarte="556", PolId = 1 , OpstinaRodjenjaId=1
                }

            };
        }

        public IEnumerable<EmailAdresa> VratiEmailAdrese()
        {
            return new List<EmailAdresa>
            {
                new EmailAdresa() { EmailAdresaId = 1,
                    NazivEmailAdrese = "prva@prva.prva",
                    TipEmailAdreseId = 1,
                    OsobaId = 1
                },
                new EmailAdresa() { EmailAdresaId = 1,
                    NazivEmailAdrese = "druga@prva.prva",
                    TipEmailAdreseId = 2,
                    OsobaId = 1
                },
                new EmailAdresa() { EmailAdresaId = 1,
                    NazivEmailAdrese = "treca@treca.treca",
                    TipEmailAdreseId = 1,
                    OsobaId = 2
                }
            };
        }

        public IEnumerable<Adresa> VratiAdrese()
        {
            return new List<Adresa>
            {
                new Adresa {AdresaId = 1,
                NazivAdrese = "Balkanska 1",
                TipAdreseId =1,
                OsobaId = 1,
                GradId = 1},

                new Adresa {AdresaId = 2,
                NazivAdrese = "Balkanska 2",
                TipAdreseId =1,
                OsobaId = 2,
                GradId = 1},
            };
        }

        public IEnumerable<MobilniTelefon> VratiMobilneTelefone()
        {
            return new List<MobilniTelefon>
            {
                new MobilniTelefon {
                    MobilniTelefonId = 1,
                    BrojMobilnog = "1234564",
                    LokalMobilniId = 1,
                    TipMobilniId = 1,
                    OsobaId = 1
                },

                new MobilniTelefon {
                    MobilniTelefonId = 2,
                    BrojMobilnog = "555333",
                    LokalMobilniId = 2,
                    TipMobilniId = 1,
                    OsobaId = 2
                },

                new MobilniTelefon {
                    MobilniTelefonId = 3,
                    BrojMobilnog = "999899",
                    LokalMobilniId = 4,
                    TipMobilniId = 2,
                    OsobaId = 2
                }
            };
        }

        public IEnumerable<FiksniTelefon> VratiFiksneTelefone()
        {
            return new List<FiksniTelefon>
            {
                new FiksniTelefon {
                    FiksniTelefonId = 1,
                    BrojFiksnog = "1234564",
                    LokalFiksniId = 1,
                    TipFiksniId = 1,
                    OsobaId = 1
                },
                new FiksniTelefon {
                    FiksniTelefonId = 2,
                    BrojFiksnog = "3332221",
                    LokalFiksniId = 2,
                    TipFiksniId = 1,
                    OsobaId = 2
                },
            };
        }

        public IEnumerable<TipEmailAdrese> VratiTipoveEmailAdrese()
        {
            return new List<TipEmailAdrese>
            {
                new TipEmailAdrese {
                    TipEmailAdreseId = 1,
                    VrstaEmailAdrese = "Privatna"
                },

                new TipEmailAdrese {
                    TipEmailAdreseId = 2,
                    VrstaEmailAdrese = "Poslovna"
                }
            };
        }

        public IEnumerable<TipAdrese> VratiTipoveAdrese()
        {
            return new List<TipAdrese>
            {
                new TipAdrese {
                    TipAdreseId = 1,
                    VrstaAdrese = "Privatna"
                },

                new TipAdrese {
                    TipAdreseId = 2,
                    VrstaAdrese = "Poslovna"
                }
            };
        }

        public IEnumerable<TipMobilni> VratiTipoveMobilnog()
        {
            return new List<TipMobilni>
            {
                new TipMobilni {
                    TipMobilniId = 1,
                    VrstaMobilni = "Privatni"
                },

                new TipMobilni {
                    TipMobilniId = 2,
                    VrstaMobilni = "Poslovni"
                }
            };
        }

        public IEnumerable<TipFiskni> VratiTipoveFiksnog()
        {
            return new List<TipFiskni>
            {
                new TipFiskni {
                    TipFiksniId = 1,
                    VrstaFiksni = "Privatni"
                },

                new TipFiskni {
                    TipFiksniId = 2,
                    VrstaFiksni = "Poslovni"
                }
            };
        }

        public IEnumerable<Opstina> VratiOpstine()
        {
            return new List<Opstina>
            {
                new Opstina
                {
                    OpstinaId = 1,
                    GradId = 1,
                    NazivOpstine = "Vracar"
                },

                new Opstina
                {
                    OpstinaId = 2,
                    GradId = 1,
                    NazivOpstine = "Palilula"
                }
            };
        }

        #endregion
    }
}