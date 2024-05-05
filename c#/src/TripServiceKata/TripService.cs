using System.Collections.Generic;
using System.Linq;
using TripServiceKata.Entity;
using TripServiceKata.Exception;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripService
    {
        private readonly IUserSession _userSession;
        private readonly ITripDaoWrapper _tripDaoWrapper;

        public TripService(IUserSession userSession, ITripDaoWrapper tripDaoWrapper)
        {
            _userSession = userSession;
            _tripDaoWrapper = tripDaoWrapper;
        }

        public List<Trip> GetTripsByUser(User user)
        {
            var loggedUser = _userSession.GetLoggedUser();
            if (loggedUser == null) throw new UserNotLoggedInException();

            var isFriend = Enumerable.Contains(user.GetFriends(), loggedUser);
            
            var tripList = isFriend ? _tripDaoWrapper.FindTripsByUser(user) : new List<Trip>();
            return tripList;
        }
    }
}