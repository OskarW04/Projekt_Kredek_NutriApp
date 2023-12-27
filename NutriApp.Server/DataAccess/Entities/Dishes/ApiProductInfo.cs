using System.ComponentModel.DataAnnotations;

namespace NutriApp.Server.DataAccess.Entities.Dishes
{
    public class ApiProductInfo
    {
        [Key] public Guid Id { get; set; }
        [MaxLength(256)] public string ApiUrl { get; set; } = default!;
        [MaxLength(256)] public string Name { get; set; } = default!;
        [MaxLength(256)] public string Brand { get; set; } = default!;

        public virtual List<DishApiProducts>? DishApiProducts { get; set; }
    }
}