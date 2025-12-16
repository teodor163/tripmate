using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class ShareServiceTests
    {
        [TestInitialize]
        public void Setup() => ShareService.ClearForTests();

        [TestMethod]
        public void Test_CreateLink_IsActive()
        {
            var l = ShareService.Create("g1", TimeSpan.FromMinutes(10));
            Assert.IsTrue(l.IsActive);
        }

        [TestMethod]
        public void Test_DisableLink_BlocksAccess()
        {
            var l = ShareService.Create("g1", TimeSpan.FromMinutes(10));
            ShareService.Disable(l.Token);
            var (ok, err, _) = ShareService.Get(l.Token);

            Assert.IsFalse(ok);
            Assert.AreEqual("Link disabled", err);
        }

        [TestMethod]
        public void Test_ExpiredLink_Fails()
        {
            var l = ShareService.Create("g1", TimeSpan.FromMilliseconds(1));
            Thread.Sleep(10);

            var (ok, err, _) = ShareService.Get(l.Token);
            Assert.IsFalse(ok);
            Assert.AreEqual("Link expired", err);
        }
    }
}
