using NutriApp.Server.DataAccess.Entities.User;

namespace NutriApp.Server.Models
{
    public class UpdateUserRequest
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Weight { get; set; }
        public int Height { get; set; }
        public UserNutritionGoalType NutritionGoal { get; set; }
    }
}