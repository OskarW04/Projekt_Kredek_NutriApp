using NutriApp.Server.Models;

namespace NutriApp.Server.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddUserDetails(string email, AddUserDetailsRequest userDetailsRequest);
        UserDto GetUserDetails(string email);
        UserDto UpdateUserDetails(string email, UpdateUserRequest updateUserDetailsRequest);
    }
}