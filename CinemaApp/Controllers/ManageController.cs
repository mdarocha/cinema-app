using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CinemaApp.Models;
using CinemaApp.Resources;
using CinemaApp.DAL;

namespace CinemaApp.Controllers
{
    [Authorize]
    public class ManageController : Controller
    {
        #region Constructors
        private IUnitOfWork db;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ManageController()
        {
            db = new UnitOfWork();
        }

        public ManageController(IUnitOfWork db)
        {
            this.db = db;
        }

        public ManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        #endregion
        //
        // GET: /Manage/Index
        public ActionResult Index(ManageMessageId? message)
        {
            if (message.HasValue) {
                ViewBag.StatusMessage = message == ManageMessageId.ChangePasswordSuccess ? Strings.ChangedPasswordSuccess :
                       message == ManageMessageId.Error ? Errors.RequestError : 
                       message == ManageMessageId.ChangeDataSuccess ? Strings.ChangePersonalDataSuccess : "";
                ViewBag.StatusIsError = (message == ManageMessageId.Error);
            }

            var user = GetUser();
            var reservations = (db.Repo<Reservation>() as IReservationsRepo).GetReservationsForUser(user)
                .ConvertAll(r => new ReservationViewModel
                {
                    ID = r.ID,
                    Showing = r.Showing,
                    ReservationDate = r.ReservationDate,
                    Places = PlacePosition.FromPlaces(r.Places)
                        .OrderBy(p => (p.y * RoomConfig.RoomWidth) + p.x)
                        .ToList(),
                    CancelUrl = Url.Action("CancelReservation", "ReservationFlow", new { id = r.ID }),
                });

            var model = new IndexViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Reservations = reservations
            };

            return View(model);
        }

        //
        // GET: /Manage/ChangePassword
        public ActionResult ChangePassword()
        {
            return View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await UserManager.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
                if (user != null)
                {
                    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                }
                return RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            AddErrors(result);
            return View(model);
        }

        //
        // GET : /Manage/ChangePersonalData
        public ActionResult ChangePersonalData()
        {
            var user = GetUser();
            var viewModel = new ChangePersonalDataViewModel
            {
                Name = user.Name,
                Surname = user.Surname,
            };

            return View(viewModel);
        }

        //
        // POST : /Manage/ChangePersonalData
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePersonalData(ChangePersonalDataViewModel model)
        {
            var user = GetUser();
            if (ModelState.IsValid)
            {
                user.Name = model.Name;
                user.Surname = model.Surname;

                UserManager.Update(user);

                return RedirectToAction("Index", new { Message = ManageMessageId.ChangeDataSuccess });
            }

            return RedirectToAction("Index", new { Message = ManageMessageId.Error });
        }


#region Pomocnicy
        // Służy do ochrony XSRF podczas dodawania logowań zewnętrznych
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = GetUser();
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private CinemaUser GetUser()
        {
            return UserManager.FindById(User.Identity.GetUserId());
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            ChangeDataSuccess,
            Error
        }

#endregion
    }
}