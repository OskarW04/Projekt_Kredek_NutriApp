using System.ComponentModel.DataAnnotations;

namespace NutriApp.Server.Models.Dish
{
    public class DishRequest
    {
        [Required] public string Name { get; set; } = default!;
        public string? Description { get; set; }
    }
}