using NutriApp.Server.DataAccess.Entities.User;

namespace NutriApp.Server.Models
{
    public class AddUserDetailsRequest
    {
        public string Name { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public int Age { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public UserNutritionGoalType NutritionGoal { get; set; }
    }
}