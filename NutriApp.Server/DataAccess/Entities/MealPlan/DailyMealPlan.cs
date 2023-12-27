using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriApp.Server.DataAccess.Entities.Meals;

namespace NutriApp.Server.DataAccess.Entities.MealPlan
{
    public class DailyMealPlan
    {
        [Key] public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Water { get; set; }
        [MaxLength(512)] public string? Notes { get; set; }

        [ForeignKey("User")] [MaxLength(450)] public string UserId { get; set; } = default!;
        public virtual User.User? User { get; set; }

        public virtual List<Meal>? Meals { get; set; }
    }
}