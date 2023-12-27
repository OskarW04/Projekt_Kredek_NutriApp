using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriApp.Server.DataAccess.Entities.Dishes;

namespace NutriApp.Server.DataAccess.Entities.Products
{
    public class Product
    {
        [Key] public Guid Id { get; set; }
        [MaxLength(256)] public string Name { get; set; } = default!;
        [MaxLength(256)] public string? Brand { get; set; }
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }
        [MaxLength(512)] public string? Ingredients { get; set; }
        public int GramsInPortion { get; set; } = 100;

        [MaxLength(450)] [ForeignKey("User")] public string UserId { get; set; } = default!;
        public virtual User.User? User { get; set; }

        public virtual List<DishProducts>? DishProducts { get; set; }
    }
}