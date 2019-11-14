using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CinemaApp.Models;

namespace CinemaApp.Tests.Models
{
    [TestClass]
    public class PlacePositionTest
    {
        [TestMethod]
        public void TestPlaceName()
        {
            Assert.AreEqual("A1", PlacePosition.PlaceName(
                new PlacePosition { x = 0, y = 0 }));
            Assert.AreEqual("L18", PlacePosition.PlaceName(
                new PlacePosition { x = 17, y = 11 }));
        }
    }
}
