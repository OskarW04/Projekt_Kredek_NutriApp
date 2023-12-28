using NutriApp.Server.Models;

namespace NutriApp.Server.Services.Interfaces
{
    public interface IUserService
    {
        UserDto GetUserDetails();
        void UpdateUserDetails(AddUserDetailsRequest addUserDetailsRequest);
    }
}