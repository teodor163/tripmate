using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Tests.Models
{
    // Testni razred za model Accommodation
    [TestClass]
    public class AccommodationTests
    {
        // 1) Ustvarjanje in nastavljanje propertyjev
        [TestMethod]
        public void Test_Accommodation_UstvarjanjeInNastavljanjeVrednosti()
        {
            // ARRANGE + ACT
            var acc = new Accommodation
            {
                DestinationName = "Maribor",
                Name = "Eco Hotel",
                IsEco = true,
                Rating = 5
            };

            // ASSERT
            Assert.AreEqual("Maribor", acc.DestinationName);
            Assert.AreEqual("Eco Hotel", acc.Name);
            Assert.IsTrue(acc.IsEco);
            Assert.AreEqual(5, acc.Rating);
        }

        // 2) Sprememba propertyjev po ustvarjanju
        [TestMethod]
        public void Test_Accommodation_SpremembaPropertyjev()
        {
            // ARRANGE
            var acc = new Accommodation();

            // ACT
            acc.DestinationName = "Ljubljana";
            acc.Name = "City Hostel";
            acc.IsEco = false;
            acc.Rating = 3;

            // ASSERT
            Assert.AreEqual("Ljubljana", acc.DestinationName);
            Assert.AreEqual("City Hostel", acc.Name);
            Assert.IsFalse(acc.IsEco);
            Assert.AreEqual(3, acc.Rating);
        }

        // 3) Rating lahko nastavimo tudi na "neobičajno" vrednost (npr. 0 ali 10)
        [TestMethod]
        public void Test_Accommodation_RatingLahkoNiMed1In5()
        {
            // ARRANGE + ACT
            var acc = new Accommodation
            {
                DestinationName = "Test",
                Name = "Weird Hotel",
                IsEco = false,
                Rating = 0 // Trenutno model to dovoli, ker ni validacije
            };

            // ASSERT
            Assert.AreEqual(0, acc.Rating);
        }
    }
}
