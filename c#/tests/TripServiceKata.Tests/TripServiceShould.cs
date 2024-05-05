using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;
using Xunit;

namespace TripServiceKata.Tests
{
    public class TripServiceShould
    {
        private class UserSessionMock : IUserSession
        {
            private readonly User _loggedUser;

            public UserSessionMock(User loggedUser)
            {
                _loggedUser = loggedUser;
            }

            public bool IsUserLoggedIn(User user)
            {
                throw new System.NotImplementedException();
            }

            public User GetLoggedUser()
            {
                return _loggedUser;
            }
        }

        private class TripDaoMock : ITripDaoWrapper
        {
            private readonly Dictionary<User, List<Trip>> _userTripMap = new Dictionary<User, List<Trip>>();

            public void AddTripsForUser(User user, List<Trip> trips)
            {
                _userTripMap[user] = trips;
            }

            public List<Trip> FindTripsByUser(User user)
            {
                return _userTripMap.ContainsKey(user) ? _userTripMap[user] : new List<Trip>();
            }
        }

        [Fact]
        public void GetTripsByUser_UserNotLoggedIn_ThrowsException()
        {
            // Arrange
            var userSession = new UserSessionMock(null);
            var tripDaoWrapper = new TripDaoMock();
            var tripService = new TripService(userSession, tripDaoWrapper);

            Assert.Throws<UserNotLoggedInException>(() => tripService.GetTripsByUser(new User()));
        }

        [Fact]
        public void GetTripsByUser_UserLoggedInButNotFriend_ReturnsEmptyList()
        {
            // Arrange
            var loggedUser = new User();
            var userSession = new UserSessionMock(loggedUser);
            var user = new User();
            var tripDaoWrapper = new TripDaoMock();
            var tripService = new TripService(userSession, tripDaoWrapper);
            
            var result = tripService.GetTripsByUser(user);
            Assert.Empty(result);
        }

        [Fact]
        public void GetTripsByUser_UserLoggedInAndIsFriend_ReturnsTripList()
        {
            // Arrange
            var loggedUser = new User();
            var userSession = new UserSessionMock(loggedUser);
            var user = new User();
            user.AddFriend(loggedUser);
            var trips = new List<Trip>() { new Trip(), new Trip() };
            var tripDaoWrapper = new TripDaoMock();
            tripDaoWrapper.AddTripsForUser(user, trips);
            var tripService = new TripService(userSession, tripDaoWrapper);
            
            var result = tripService.GetTripsByUser(user);
            Assert.Equal(trips, result);
        }
    }
}