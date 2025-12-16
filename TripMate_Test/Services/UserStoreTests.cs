using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class UserStoreTests
    {
        [TestInitialize]
        public void Setup() => UserStore.ClearForTests();

        [TestMethod]
        public void Test_EmailExists_ReturnsTrueAfterAdd()
        {
            UserStore.Add(new UserAccount { Email = "x@test.com", Password = "1", Name = "X" });
            Assert.IsTrue(UserStore.EmailExists("x@test.com"));
        }

        [TestMethod]
        public void Test_FindByEmail_ReturnsUser()
        {
            UserStore.Add(new UserAccount { Email = "x@test.com", Password = "1", Name = "X" });
            var u = UserStore.FindByEmail("x@test.com");
            Assert.IsNotNull(u);
            Assert.AreEqual("X", u!.Name);
        }

        [TestMethod]
        public void Test_Update_ChangesNameAndInterests()
        {
            UserStore.Add(new UserAccount { Email = "x@test.com", Password = "1", Name = "X", Interests = "" });
            UserStore.Update(new UserAccount { Email = "x@test.com", Name = "New", Interests = "nature" });

            var u = UserStore.FindByEmail("x@test.com")!;
            Assert.AreEqual("New", u.Name);
            Assert.AreEqual("nature", u.Interests);
        }
    }
}
