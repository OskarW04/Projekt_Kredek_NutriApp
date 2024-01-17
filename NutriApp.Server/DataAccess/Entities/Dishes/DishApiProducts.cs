using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NutriApp.Server.DataAccess.Entities.Dishes
{
    public class DishApiProducts
    {
        [Key] public Guid Id { get; set; }
        [ForeignKey("Dish")] public Guid? DishId { get; set; }
        [ForeignKey("ApiProductInfo")] public Guid ApiProductInfoId { get; set; }
        public int Amount { get; set; } = 100;

        public virtual ApiProductInfo? ApiProductInfo { get; set; }
        public virtual Dish? Dish { get; set; }
    }
}