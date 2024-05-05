using System.Collections.Generic;
using TripServiceKata.Entity;

namespace TripServiceKata
{
    public interface ITripDaoWrapper
    {
        List<Trip> FindTripsByUser(User user);
    }
}