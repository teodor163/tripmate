using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class VotingServiceTests
    {
        [TestInitialize]
        public void Setup() => VotingService.ClearForTests();

        [TestMethod]
        public void Test_CastVote_AddsVote()
        {
            VotingService.CastVote("g1", "a@test.com", "Bled");
            var counts = VotingService.GetCounts("g1");
            Assert.AreEqual(1, counts["Bled"]);
        }

        [TestMethod]
        public void Test_ChangeVote_UpdatesVote()
        {
            VotingService.CastVote("g1", "a@test.com", "Bled");
            VotingService.CastVote("g1", "a@test.com", "Ljubljana");
            var counts = VotingService.GetCounts("g1");

            Assert.IsFalse(counts.ContainsKey("Bled"));
            Assert.AreEqual(1, counts["Ljubljana"]);
        }

        [TestMethod]
        public void Test_TieDetected()
        {
            VotingService.CastVote("g1", "a@test.com", "A");
            VotingService.CastVote("g1", "b@test.com", "B");

            var (tie, winners, top) = VotingService.GetWinners("g1");
            Assert.IsTrue(tie);
            Assert.AreEqual(1, top);
            Assert.AreEqual(2, winners.Count);
        }
    }
}
