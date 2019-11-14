using Microsoft.Owin;
using Owin;
using CinemaApp.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using CinemaApp.DAL;

[assembly: OwinStartupAttribute(typeof(CinemaApp.Startup))]
namespace CinemaApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRoles();
        }

        private void createRoles()
        {
            CinemaDbContext storage = new CinemaDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(storage));
            var userManager = new UserManager<CinemaUser>(new UserStore<CinemaUser>(storage));

            if(!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

        }
    }
}
