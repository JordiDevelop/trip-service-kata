using System.Collections.Generic;
using TripServiceKata.Entity;
using TripServiceKata.Service;

namespace TripServiceKata
{
    public class TripDaoWrapper : ITripDaoWrapper
    {
        public TripDaoWrapper()
        {
        }

        public List<Trip> FindTripsByUser(User user)
        {
            return TripDAO.FindTripsByUser(user);
        }
    }
}