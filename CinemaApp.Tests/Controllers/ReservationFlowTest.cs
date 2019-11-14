using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaApp.Controllers;
using System.Web.Mvc;
using CinemaApp.Tests.DAL;
using CinemaApp.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web.Helpers;
using CinemaApp.DAL;
using NSubstitute;

namespace CinemaApp.Tests.Controllers
{
    [TestClass]
    public class ReservationFlowTest : MockControllerBase<ReservationFlowController>
    {
        public ReservationFlowController CreateController(IUnitOfWork db = null, IMailSender mail = null, Func<string> idFunc = null)
        {
            mail = mail ?? new MockMailSender();
            idFunc = idFunc ?? (() => "testId");
            return base.CreateController(db, mail, idFunc);
        }

        [TestMethod]
        public void TestIndex()
        {
            ReservationFlowController reservationFlow = CreateController();

            ViewResult result = reservationFlow.Index(0) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Index");
        }

        #region MakeReservation tests
        [TestMethod]
        public void TestMakeReservation()
        {
            JsonResult result = MakeReservationHelper(new ReservationFlowViewModel());
            dynamic data = result.Data;

            Assert.IsTrue(data.success);
        }

        [TestMethod]
        public void TestMakeReservationDuplicatePlaces()
        {
            JsonResult result = MakeReservationHelper(new ReservationFlowViewModel
            {
                TakenPlaces = new List<PlacePosition> { new PlacePosition { x = 0, y = 0 }, new PlacePosition { x = 1, y = 0 } },
                SelectedPlaces = new List<PlacePosition> { new PlacePosition { x = 1, y = 0 } },
            });
            dynamic data = result.Data;

            Assert.IsFalse(data.success);
        }

        [TestMethod]
        public void TestMakeReservationShowingTooLate()
        {
            JsonResult result = MakeReservationHelper(new ReservationFlowViewModel
            {
                Showing = new Showing { Time = DateTime.Now.AddHours(-1) }
            });
            dynamic data = result.Data;

            Assert.IsFalse(data.success);
        }

        private JsonResult MakeReservationHelper(ReservationFlowViewModel model)
        {
            var db = new MockUnitOfWork();

            model.Movie = model.Movie ?? new Movie();
            model.Showing = model.Showing ?? db.Repo<Showing>().Get(0);
            model.Showing.Movie = model.Showing.Movie ?? new Movie();

            model.TakenPlaces = model.TakenPlaces ?? (db.Repo<Place>() as IPlacesRepo).GetTakenPlaces(0);
            model.SelectedPlaces = model.SelectedPlaces ?? new List<PlacePosition> {
                new PlacePosition { x = 0, y = 1 } };
            model.PostUrl = model.PostUrl ?? "test";

            (db.Repo<Showing>() as MockShowingsRepo).Showing = model.Showing;

            var reservationFlow = CreateController(db);
            return reservationFlow.MakeReservation(model) as JsonResult;
        }
        #endregion

        [TestMethod]
        public void TestCancelReservation()
        {
            var reservationFlow = CreateController();

            var result = reservationFlow.CancelReservation(0) as JsonResult;
            dynamic data = result.Data;

            Assert.IsTrue(data.success);
        }

        [TestMethod]
        public void TestCancelReservationInvalidUser()
        {
            var reservationFlow = CreateController(null, null, (() => "another user"));

            var result = reservationFlow.CancelReservation(0) as JsonResult;
            dynamic data = result.Data;

            Assert.IsFalse(data.success);
        }

        [TestMethod]
        public void TestCancelReservationTooLate()
        {
            var db = new MockUnitOfWork();
            (db.Repo<Reservation>() as MockReservationRepo).Reservation.Showing.Time = DateTime.Now.AddMinutes(20);
            var reservationFlow = CreateController(db);

            var result = reservationFlow.CancelReservation(0) as JsonResult;
            dynamic data = result.Data;

            Assert.IsFalse(data.success);
        }
    }
}
