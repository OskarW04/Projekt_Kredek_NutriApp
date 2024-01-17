using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriApp.Server.DataAccess.Entities.Meals;

namespace NutriApp.Server.DataAccess.Entities.Dishes
{
    // Danie użytkownika sklada sie z produktów (dodanych przez użytkownika lub pochodzące z API)

    public class Dish
    {
        [Key] public Guid Id { get; set; }
        [MaxLength(450)] [ForeignKey("User")] public string UserId { get; set; } = default!;
        [MaxLength(256)] public string Name { get; set; } = default!;
        [MaxLength(512)] public string? Description { get; set; } = default!;
        public int GramsTotal { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }

        public virtual User.User? User { get; set; }
        public virtual List<Meal>? Meals { get; set; }

        public virtual List<DishProducts>? DishProducts { get; set; }
        public virtual List<DishApiProducts>? DishApiProducts { get; set; }
    }
}