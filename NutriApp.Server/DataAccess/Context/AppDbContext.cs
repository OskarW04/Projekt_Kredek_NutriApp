using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NutriApp.Server.DataAccess.Entities.Dishes;
using NutriApp.Server.DataAccess.Entities.MealPlan;
using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.DataAccess.Entities.User;

namespace NutriApp.Server.DataAccess.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public DbSet<UserDetails> UserDetails { get; set; }
        public DbSet<DailyMealPlan> DailyMealPlans { get; set; }
        public DbSet<Meal> Meals { get; set; }
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ApiProductInfo> ApiProductInfos { get; set; }
        public DbSet<DishProducts> DishProducts { get; set; }
        public DbSet<DishApiProducts> DishApiProducts { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserDetails>()
                .HasOne(x => x.User)
                .WithOne(x => x.UserDetails)
                .HasForeignKey<UserDetails>(x => x.UserId);

            builder.Entity<UserDetails>()
                .Property(x => x.NutritionGoalType)
                .HasConversion<string>();

            builder.Entity<DailyMealPlan>()
                .HasOne(x => x.User)
                .WithMany(x => x.DailyMealPlans)
                .HasForeignKey(x => x.UserId);

            builder.Entity<DailyMealPlan>()
                .HasMany(x => x.Meals)
                .WithOne(x => x.DailyMealPlan)
                .HasForeignKey(x => x.DailyMealPlanId);

            builder.Entity<Meal>()
                .HasOne(x => x.Dish)
                .WithMany(x => x.Meals)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Meal>()
                .Property(x => x.MealType)
                .HasConversion<string>();

            builder.Entity<Dish>()
                .HasOne(x => x.User)
                .WithMany(x => x.Dishes)
                .HasForeignKey(x => x.UserId);

            builder.Entity<Dish>()
                .HasMany(x => x.DishProducts)
                .WithOne(x => x.Dish)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Dish>()
                .HasMany(x => x.DishApiProducts)
                .WithOne(x => x.Dish)
                .HasForeignKey(x => x.DishId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Product>()
                .HasMany(x => x.DishProducts)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

            builder.Entity<Product>()
                .HasOne(x => x.User)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.UserId);

            builder.Entity<ApiProductInfo>()
                .HasMany(x => x.DishApiProducts)
                .WithOne(x => x.ApiProductInfo)
                .HasForeignKey(x => x.ApiProductInfoId);


            base.OnModelCreating(builder);
        }
    }
}