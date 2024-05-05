using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        [Fact]
        public void GetTripsByUser()
        {
            var userSessionMock = new UserSessionMock();
            var tripDaoWrapper = new TripDaoWrapperMock();
            var tripService = new TripService(userSessionMock, tripDaoWrapper);
            var userMock = new User();
            Assert.Empty(tripService.GetTripsByUser(userMock));
        }
    }

    public class TripDaoWrapperMock : ITripDaoWrapper
    {
        public List<Trip> FindTripsByUser(User user)
        {
            throw new System.NotImplementedException();
        }
    }

    public class UserSessionMock : IUserSession
    {
        public bool IsUserLoggedIn(User user)
        {
            throw new System.NotImplementedException();
        }

        public User GetLoggedUser()
        {
            throw new System.NotImplementedException();
        }
    }
}
