using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ProjektniZadatak.Models;

[assembly: OwinStartupAttribute(typeof(ProjektniZadatak.Startup))]
namespace ProjektniZadatak
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            KreirajPravoPristupa();
        }


        private void KreirajPravoPristupa()
        {
            ApplicationDbContext context = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            


            // Kreiranje prava pristupa "Pravo administracije"  
            if (!roleManager.RoleExists("Pravo administracije"))
            {

                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pravo administracije";
                roleManager.Create(role);

            }

            // Kreiranje prava pristupa "Pravo unosa"   
            if (!roleManager.RoleExists("Pravo unosa"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pravo unosa";
                roleManager.Create(role);

            }

            // Kreiranje prava pristupa "Pravo pregleda"   
            if (!roleManager.RoleExists("Pravo pregleda"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Pravo pregleda";
                roleManager.Create(role);

            }
        }
    }
}
