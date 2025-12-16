using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class AuthServiceTests
    {
        [TestInitialize]
        public void Setup() => UserStore.ClearForTests();

        [TestMethod]
        public void Test_Register_Success()
        {
            var (ok, error) = AuthService.Register("a@test.com", "123", "Ana");
            Assert.IsTrue(ok);
            Assert.AreEqual("", error);
        }

        [TestMethod]
        public void Test_Register_MissingData_Fails()
        {
            var (ok, error) = AuthService.Register("", "123", "Ana");
            Assert.IsFalse(ok);
            Assert.AreEqual("Missing data", error);
        }

        [TestMethod]
        public void Test_Register_DuplicateEmail_Fails()
        {
            AuthService.Register("a@test.com", "123", "Ana");
            var (ok, error) = AuthService.Register("a@test.com", "999", "Ana2");
            Assert.IsFalse(ok);
            Assert.AreEqual("Email already used", error);
        }
    }
}
