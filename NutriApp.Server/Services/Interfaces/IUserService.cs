using NutriApp.Server.Models;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserDetails();
        void AddUserDetails(AddUserDetailsRequest addUserDetailsRequest);
        UserDto UpdateUserDetails(UpdateUserRequest updateUserRequest);
    }
}