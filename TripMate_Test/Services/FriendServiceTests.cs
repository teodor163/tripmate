using Microsoft.VisualStudio.TestTools.UnitTesting;
using TripMate_TeodorLazar.Models;
using TripMate_TeodorLazar.Services;

namespace TripMate_TeodorLazar.Tests.Services
{
    [TestClass]
    public class FriendServiceTests
    {
        [TestInitialize]
        public void Setup()
        {
            UserStore.ClearForTests();
            GroupService.ClearForTests();
            FriendService.ClearForTests();

            // create two users
            UserStore.Add(new UserAccount { Email = "a@test.com", Password = "1", Name = "A" });
            UserStore.Add(new UserAccount { Email = "b@test.com", Password = "1", Name = "B" });

            GroupService.EnsureDefaultGroup("a@test.com");
            GroupService.EnsureDefaultGroup("b@test.com");
        }

        [TestMethod]
        public void Test_SendInvite_ValidUser_Succeeds()
        {
            var g = GroupService.GetGroupsFor("a@test.com").First();
            var (ok, _, inv) = FriendService.SendInvite("a@test.com", "b@test.com", g.Id);
            Assert.IsTrue(ok);
            Assert.IsNotNull(inv);
            Assert.AreEqual(InviteStatus.Pending, inv!.Status);
        }

        [TestMethod]
        public void Test_SendInvite_NonExistingUser_Fails()
        {
            var g = GroupService.GetGroupsFor("a@test.com").First();
            var (ok, err, _) = FriendService.SendInvite("a@test.com", "no@test.com", g.Id);
            Assert.IsFalse(ok);
            Assert.AreEqual("User does not exist", err);
        }

        [TestMethod]
        public void Test_RejectInvite_Works()
        {
            var g = GroupService.GetGroupsFor("a@test.com").First();
            var (ok, _, inv) = FriendService.SendInvite("a@test.com", "b@test.com", g.Id);
            Assert.IsTrue(ok);

            var (ok2, _) = FriendService.Respond(inv!.Id, "b@test.com", accept: false);
            Assert.IsTrue(ok2);

            var incoming = FriendService.GetIncoming("b@test.com");
            Assert.AreEqual(InviteStatus.Rejected, incoming.First().Status);
        }
    }
}
