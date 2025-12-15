using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Tests.Models
{
    // Testni razred za model Stop
    [TestClass]
    public class StopTests
    {
        // 1) Ustvarjanje z imenom
        [TestMethod]
        public void Test_Stop_UstvarjanjeZNekomImenom()
        {
            // ARRANGE + ACT
            var stop = new Stop
            {
                Name = "Bus Station"
            };

            // ASSERT
            Assert.AreEqual("Bus Station", stop.Name);
        }

        // 2) Sprememba imena po ustvarjanju
        [TestMethod]
        public void Test_Stop_SpremembaImena()
        {
            // ARRANGE
            var stop = new Stop();

            // ACT
            stop.Name = "Train Station";

            // ASSERT
            Assert.AreEqual("Train Station", stop.Name);
        }

        // 3) Prazen string kot ime postaje
        [TestMethod]
        public void Test_Stop_PrazenNameJeDovoljen()
        {
            // ARRANGE + ACT
            var stop = new Stop
            {
                Name = ""
            };

            // ASSERT
            Assert.AreEqual("", stop.Name);
        }
    }
}
