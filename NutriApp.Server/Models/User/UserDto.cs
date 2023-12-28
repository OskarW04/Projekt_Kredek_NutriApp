using NutriApp.Server.DataAccess.Entities.User;

namespace NutriApp.Server.Models.User
{
    public class UserDto
    {
        public required string Email { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int Age { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public string? NutritionGoal { get; set; }
    }
}