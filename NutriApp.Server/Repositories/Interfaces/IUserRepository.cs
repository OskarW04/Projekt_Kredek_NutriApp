using NutriApp.Server.DataAccess.Entities.User;
using NutriApp.Server.Models.User;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUserDetails(string email, UserDetailsRequest userDetailsRequest);
        UserDto GetUserDetails(string email);
        UserDto UpdateUserDetails(string email, UserDetailsRequest updateUserDetailsRequest);
        User? GetByEmail(string email);
    }
}