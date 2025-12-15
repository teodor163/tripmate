using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Tests.Models
{
    // Testni razred za model WeatherInfo
    [TestClass]
    public class WeatherInfoTests
    {
        // 1) Ustvarjanje in nastavljanje vrednosti
        [TestMethod]
        public void Test_WeatherInfo_UstvarjanjeInNastavljanjeVrednosti()
        {
            // ARRANGE + ACT
            var w = new WeatherInfo
            {
                Destination = "Maribor",
                Condition = "sunny",
                Temperature = 25
            };

            // ASSERT
            Assert.AreEqual("Maribor", w.Destination);
            Assert.AreEqual("sunny", w.Condition);
            Assert.AreEqual(25, w.Temperature);
        }

        // 2) Sprememba vrednosti po ustvarjanju
        [TestMethod]
        public void Test_WeatherInfo_SpremembaVrednosti()
        {
            // ARRANGE
            var w = new WeatherInfo();

            // ACT
            w.Destination = "Ljubljana";
            w.Condition = "rain";
            w.Temperature = 10;

            // ASSERT
            Assert.AreEqual("Ljubljana", w.Destination);
            Assert.AreEqual("rain", w.Condition);
            Assert.AreEqual(10, w.Temperature);
        }

        // 3) Negativna temperatura (trenutno dovoli tudi to)
        [TestMethod]
        public void Test_WeatherInfo_NegativnaTemperaturaJeDovoljena()
        {
            // ARRANGE + ACT
            var w = new WeatherInfo
            {
                Destination = "Antarctica",
                Condition = "snow",
                Temperature = -30
            };

            // ASSERT
            Assert.AreEqual(-30, w.Temperature);
        }
    }
}
