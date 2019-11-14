using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using CinemaApp.Tests.DAL;
using CinemaApp.Models;
using CinemaApp.DAL;

namespace CinemaApp.Tests.Controllers
{
    public class MockControllerBase<T> where T: Controller, new()
    {
        public virtual T CreateController(IUnitOfWork db = null, params object[] optionalArgs)
        {
            db = db ?? new MockUnitOfWork();

            object[] args = new object[optionalArgs.Length + 1];
            args[0] = db;
            for (int i = 1; i <= optionalArgs.Length; i++)
                args[i] = optionalArgs[i-1];

            T controller = Activator.CreateInstance(typeof(T), args) as T;
            controller.Url = UrlMock;
            controller.ControllerContext = AuthenticatedControllerContext;

            return controller;
        }

        public UrlHelper UrlMock
        {
            get
            {
                var url = Substitute.For<UrlHelper>(
                    new RequestContext(Substitute.For<HttpContextBase>(), new RouteData()),
                    new RouteCollection());
                url.Content("").ReturnsForAnyArgs("test content");
                return url;
            }
        }

        public ControllerContext AuthenticatedControllerContext
        {
            get {
                var mock = Substitute.For<ControllerContext>();
                mock.HttpContext.User.Identity.Name.Returns("test@test.com");

                mock.HttpContext.Request.IsAuthenticated.Returns(true);
                return mock;
            }
        }
    }
}
