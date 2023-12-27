using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NutriApp.Server.DataAccess.Entities.Products;

namespace NutriApp.Server.DataAccess.Entities.Dishes
{
    public class DishProducts
    {
        [Key] public Guid Id { get; set; }
        [ForeignKey("Dish")] public Guid DishId { get; set; }
        [ForeignKey("Product")] public Guid ProductId { get; set; }

        public virtual Product? Product { get; set; }
        public virtual Dish? Dish { get; set; }
    }
}