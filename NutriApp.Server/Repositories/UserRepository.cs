using Microsoft.EntityFrameworkCore;
using NutriApp.Server.DataAccess.Context;
using NutriApp.Server.DataAccess.Entities.User;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models.User;
using NutriApp.Server.Repositories.Interfaces;

namespace NutriApp.Server.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void AddUserDetails(string email, UserDetailsRequest userDetailsRequest)
        {
            var userByEmail = _appDbContext.Users
                .Include(x => x.UserDetails)
                .FirstOrDefault(x => x.Email == email);

            if (userByEmail is null)
            {
                throw new ResourceNotFoundException($"User with email {email} not found");
            }

            if (userByEmail.UserDetails is not null)
            {
                throw new ResourceAlreadyExistsException($"User with email {email} already has details");
            }

            userByEmail.Name = userDetailsRequest.Name;
            userByEmail.LastName = userDetailsRequest.LastName;

            var userDetails = new UserDetails
            {
                Id = Guid.NewGuid(),
                UserId = userByEmail.Id,
                Age = userDetailsRequest.Age,
                Height = userDetailsRequest.Height,
                Weight = userDetailsRequest.Weight,
                NutritionGoalType = userDetailsRequest.NutritionGoal,
            };

            _appDbContext.UserDetails.Add(userDetails);
            _appDbContext.SaveChanges();
        }

        public UserDto GetUserDetails(string email)
        {
            var userByEmail = _appDbContext.Users
                .Include(x => x.UserDetails)
                .FirstOrDefault(x => x.Email == email);

            if (userByEmail?.UserDetails is null)
            {
                throw new ResourceNotFoundException($"User with email {email} not found");
            }

            return new UserDto()
            {
                Email = userByEmail.Email,
                Name = userByEmail.Name,
                LastName = userByEmail.LastName,
                Age = userByEmail.UserDetails.Age,
                Weight = userByEmail.UserDetails.Weight,
                Height = userByEmail.UserDetails.Height,
                NutritionGoal = userByEmail.UserDetails.NutritionGoalType.ToString(),
            };
        }

        public UserDto UpdateUserDetails(string email, UserDetailsRequest updateUserDetailsRequest)
        {
            var userByEmail = _appDbContext.Users
                .Include(x => x.UserDetails)
                .FirstOrDefault(x => x.Email == email);

            if (userByEmail?.UserDetails is null)
            {
                throw new ResourceNotFoundException($"User with email {email} not found");
            }

            userByEmail.Name = updateUserDetailsRequest.Name;
            userByEmail.LastName = updateUserDetailsRequest.LastName;
            userByEmail.UserDetails.Age = updateUserDetailsRequest.Age;
            userByEmail.UserDetails.Weight = updateUserDetailsRequest.Weight;
            userByEmail.UserDetails.Height = updateUserDetailsRequest.Height;
            userByEmail.UserDetails.NutritionGoalType = updateUserDetailsRequest.NutritionGoal;

            _appDbContext.SaveChanges();

            return new UserDto()
            {
                Email = userByEmail.Email,
                Name = userByEmail.Name,
                LastName = userByEmail.LastName,
                Age = userByEmail.UserDetails.Age,
                Weight = userByEmail.UserDetails.Weight,
                Height = userByEmail.UserDetails.Height,
                NutritionGoal = userByEmail.UserDetails.NutritionGoalType.ToString(),
            };
        }

        public User? GetByEmail(string email)
        {
            return _appDbContext.Users
                .FirstOrDefault(x => x.Email == email);
        }
    }
}