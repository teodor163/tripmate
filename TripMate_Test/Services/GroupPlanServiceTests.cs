using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class GroupPlanServiceTests
    {
        [TestInitialize]
        public void Setup() => GroupPlanService.ClearForTests();

        [TestMethod]
        public void Test_AddStop_IncrementsVersion()
        {
            var p = GroupPlanService.GetOrCreate("g1");
            var (ok, _, plan) = GroupPlanService.AddStop("g1", p.Version, "a@test.com", "Bled");
            Assert.IsTrue(ok);
            Assert.AreEqual(1, plan.Version);
            Assert.AreEqual(1, plan.Stops.Count);
        }

        [TestMethod]
        public void Test_Conflict_WhenClientVersionOld()
        {
            var p = GroupPlanService.GetOrCreate("g1");
            GroupPlanService.AddStop("g1", p.Version, "a@test.com", "Bled"); // version now 1

            var (ok, err, _) = GroupPlanService.AddStop("g1", 0, "b@test.com", "Ljubljana");
            Assert.IsFalse(ok);
            Assert.IsTrue(err.StartsWith("CONFLICT"));
        }

        [TestMethod]
        public void Test_MoveUp_ChangesOrder()
        {
            var p = GroupPlanService.GetOrCreate("g1");
            var v0 = p.Version;
            GroupPlanService.AddStop("g1", v0, "a@test.com", "A"); // v1
            var v1 = GroupPlanService.GetOrCreate("g1").Version;
            GroupPlanService.AddStop("g1", v1, "a@test.com", "B"); // v2

            var plan = GroupPlanService.GetOrCreate("g1");
            var (ok, _, after) = GroupPlanService.MoveUp("g1", plan.Version, "a@test.com", 1);
            Assert.IsTrue(ok);
            Assert.AreEqual("B", after.Stops[0]);
        }
    }
}
