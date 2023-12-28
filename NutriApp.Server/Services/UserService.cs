using NutriApp.Server.Exceptions;
using NutriApp.Server.Models;
using NutriApp.Server.Repositories.Interfaces;
using NutriApp.Server.Services.Interfaces;

namespace NutriApp.Server.Services
{
    public class UserService : IUserService
    {
        private readonly IUserContextService _userContextService;
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository,
            IUserContextService userContextService)
        {
            _userContextService = userContextService;
            _userRepository = userRepository;
        }

        public UserDto GetUserDetails()
        {
            var userEmail = _userContextService.Email;
            if (userEmail is null)
            {
                throw new ForbidException("User claims invalid");
            }

            return _userRepository.GetUserDetails(userEmail);
        }

        public void AddUserDetails(AddUserDetailsRequest addUserDetailsRequest)
        {
            var userEmail = _userContextService.Email;
            if (userEmail is null)
            {
                throw new ForbidException("User claims invalid");
            }

            _userRepository.AddUserDetails(userEmail, addUserDetailsRequest);
        }

        public UserDto UpdateUserDetails(UpdateUserRequest updateUserRequest)
        {
            var userEmail = _userContextService.Email;
            if (userEmail is null)
            {
                throw new ForbidException("User claims invalid");
            }

            return _userRepository.UpdateUserDetails(userEmail, updateUserRequest);
        }
    }
}