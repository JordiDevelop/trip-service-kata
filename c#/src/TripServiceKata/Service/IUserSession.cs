using TripServiceKata.Entity;

namespace TripServiceKata.Service
{
    public interface IUserSession
    {
        bool IsUserLoggedIn(User user);
        User GetLoggedUser();
    }
}