using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Tests.Models
{
    // Testni razred za model Destination
    [TestClass]
    public class DestinationTests
    {
        // 1) Test: ustvarjanje objekta in nastavljanje propertyjev
        [TestMethod]
        public void Test_Destination_UstvarjanjeInNastavljanjeVrednosti()
        {
            // ARRANGE + ACT
            var destination = new Destination
            {
                Name = "Maribor",
                Country = "Slovenia",
                Category = "food"
            };

            // ASSERT
            Assert.AreEqual("Maribor", destination.Name);
            Assert.AreEqual("Slovenia", destination.Country);
            Assert.AreEqual("food", destination.Category);
        }

        // 2) Test: kasnejša sprememba propertyjev
        [TestMethod]
        public void Test_Destination_SpremembaPropertyjev()
        {
            // ARRANGE
            var destination = new Destination();

            // ACT
            destination.Name = "Ljubljana";
            destination.Country = "Slovenia";
            destination.Category = "culture";

            // ASSERT
            Assert.AreEqual("Ljubljana", destination.Name);
            Assert.AreEqual("Slovenia", destination.Country);
            Assert.AreEqual("culture", destination.Category);
        }

        // 3) Test: prazen string kot ime destinacije (trenutno je to dovoljeno)
        [TestMethod]
        public void Test_Destination_PrazenNameJeDovoljen()
        {
            // ARRANGE + ACT
            var destination = new Destination
            {
                Name = "",
                Country = "Nowhere",
                Category = "unknown"
            };

            // ASSERT: preverimo samo, da shrani prazen string in ne crashne
            Assert.AreEqual("", destination.Name);
            Assert.AreEqual("Nowhere", destination.Country);
            Assert.AreEqual("unknown", destination.Category);
        }
    }
}
