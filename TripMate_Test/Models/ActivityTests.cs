using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;

namespace TripMate_TeodorLazar.Tests.Models
{
    // Testni razred za model Activity
    [TestClass]
    public class ActivityTests
    {
        // 1) Ustvarjanje in nastavljanje vrednosti
        [TestMethod]
        public void Test_Activity_UstvarjanjeInNastavljanjeVrednosti()
        {
            // ARRANGE + ACT
            var activity = new Activity
            {
                Destination = "Bled",
                Name = "Hiking",
                DistanceKm = 10
            };

            // ASSERT
            Assert.AreEqual("Bled", activity.Destination);
            Assert.AreEqual("Hiking", activity.Name);
            Assert.AreEqual(10, activity.DistanceKm);
        }

        // 2) Sprememba propertyjev po ustvarjanju
        [TestMethod]
        public void Test_Activity_SpremembaPropertyjev()
        {
            // ARRANGE
            var activity = new Activity();

            // ACT
            activity.Destination = "Piran";
            activity.Name = "Swimming";
            activity.DistanceKm = 2;

            // ASSERT
            Assert.AreEqual("Piran", activity.Destination);
            Assert.AreEqual("Swimming", activity.Name);
            Assert.AreEqual(2, activity.DistanceKm);
        }

        // 3) Negativna razdalja (trenutno jo model dovoli, ker ni validacije)
        [TestMethod]
        public void Test_Activity_NegativnaRazdaljaJeDovoljena()
        {
            // ARRANGE + ACT
            var activity = new Activity
            {
                Destination = "Test",
                Name = "Strange Activity",
                DistanceKm = -5
            };

            // ASSERT
            Assert.AreEqual(-5, activity.DistanceKm);
        }
    }
}
