using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using NutriApp.Server.DataAccess.Entities.Dishes;
using NutriApp.Server.DataAccess.Entities.MealPlan;
using NutriApp.Server.DataAccess.Entities.Products;

namespace NutriApp.Server.DataAccess.Entities.User
{
    public class User : IdentityUser
    {
        [MaxLength(256)] public string Name { get; set; } = default!;
        [MaxLength(256)] public string LastName { get; set; } = default!;
        public DateTime AccountCreationDate { get; set; } = DateTime.Now;

        public virtual UserDetails? UserDetails { get; set; }
        public virtual List<DailyMealPlan>? DailyMealPlans { get; set; }
        public virtual List<Dish>? Dishes { get; set; }
        public virtual List<Product>? Products { get; set; }
    }
}