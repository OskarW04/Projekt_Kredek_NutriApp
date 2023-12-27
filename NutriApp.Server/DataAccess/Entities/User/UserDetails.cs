using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriApp.Server.DataAccess.Entities.User
{
    public class UserDetails
    {
        public Guid Id { get; set; }
        [ForeignKey("User")] [MaxLength(450)] public string UserId { get; set; } = default!;
        public int Age { get; set; }
        public int Height { get; set; }
        public decimal Weight { get; set; }
        public UserNutritionGoalType NutritionGoalType { get; set; }

        public virtual User? User { get; set; }
    }
}