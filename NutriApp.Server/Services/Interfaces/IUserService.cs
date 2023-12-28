using NutriApp.Server.Models.User;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserDetails();
        void AddUserDetails(UserDetailsRequest addUserDetailsRequest);
        UserDto UpdateUserDetails(UserDetailsRequest updateUserRequest);
    }
}