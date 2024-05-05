using TripServiceKata.Entity;

namespace TripServiceKata.Service
{
    public interface IUserSession
    {
        User GetLoggedUser();
    }
}