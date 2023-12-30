using System.ComponentModel.DataAnnotations;

namespace NutriApp.Server.DataAccess.Entities.Dishes
{
    public class ApiProductInfo
    {
        [Key] public Guid Id { get; set; }

        [MaxLength(256)] public string ApiUrl { get; set; } = default!;
        [MaxLength(256)] public string ApiId { get; set; } = default!;
        [MaxLength(256)] public string Name { get; set; } = default!;
        [MaxLength(256)] public string? Brand { get; set; } = default!;
        [MaxLength(512)] public string Description { get; set; } = default!;
        [MaxLength(512)] public string Portion { get; set; } = default!;

        public int GramsInPortion { get; set; } = 100;
        public int Calories { get; set; }
        public int Proteins { get; set; }
        public int Carbohydrates { get; set; }
        public int Fats { get; set; }

        public virtual List<DishApiProducts>? DishApiProducts { get; set; }
    }
}