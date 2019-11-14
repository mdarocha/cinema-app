using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using CinemaApp.Models;
using CinemaApp.DAL;

namespace CinemaApp
{
    public partial class Startup
    {
        // Aby uzyskać więcej informacji o konfigurowaniu uwierzytelniania, odwiedź stronę https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Skonfiguruj kontekst bazy danych, menedżera użytkowników i menedżera logowania, aby używać jednego wystąpienia na żądanie
            app.CreatePerOwinContext(CinemaDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Zezwalaj aplikacji na przechowywanie w pliku cookie informacji o zalogowanym użytkowniku
            // oraz na tymczasowe przechowywanie w pliku cookie informacji o użytkowniku logującym się przy użyciu dostawcy logowania innego producenta
            // Konfiguruj plik cookie logowania
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Umożliwia aplikacji weryfikowanie znacznika zabezpieczeń podczas logowania się użytkownika.
                    // Jest to funkcja zabezpieczeń używana w przypadku zmiany hasła lub dodawania logowania zewnętrznego do konta.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, CinemaUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
       }
    }
}