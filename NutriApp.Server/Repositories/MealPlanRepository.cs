﻿using Microsoft.EntityFrameworkCore;
using NutriApp.Server.DataAccess.Context;
using NutriApp.Server.DataAccess.Entities.MealPlan;
using NutriApp.Server.DataAccess.Entities.Meals;
using NutriApp.Server.Exceptions;
using NutriApp.Server.Models.Dish;
using NutriApp.Server.Models.MealPlan;
using NutriApp.Server.Models.Product;
using NutriApp.Server.Repositories.Interfaces;

namespace NutriApp.Server.Repositories
{
    public class MealPlanRepository : IMealPlanRepository
    {
        private readonly AppDbContext _dbContext;

        public MealPlanRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public MealPlanDto? GetByDate(DateTime date, string userId)
        {
            var mealPlan = _dbContext.DailyMealPlans
                .Include(mp => mp.Meals)!
                .ThenInclude(m => m.Dish)
                .ThenInclude(d => d.DishProducts)
                .ThenInclude(dp => dp.Product)
                .Include(mp => mp.Meals)
                .ThenInclude(m => m.Dish)
                .ThenInclude(d => d.DishApiProducts)
                .ThenInclude(dap => dap.ApiProductInfo)
                .FirstOrDefault(mp => mp.Date == date && mp.UserId == userId);

            if (mealPlan is null)
            {
                var newMealPlan = CreateMealPlan(date, userId);
                return new MealPlanDto()
                {
                    Id = newMealPlan.Id,
                    Date = newMealPlan.Date,
                    WaterConsumption = newMealPlan.Water,
                    Notes = newMealPlan.Notes,
                    Meals = [],
                    CaloriesTotal = 0,
                    CarbohydratesTotal = 0,
                    ProteinsTotal = 0,
                    FatsTotal = 0,
                };
            }

            return new MealPlanDto()
            {
                Id = mealPlan.Id,
                Date = mealPlan.Date,
                WaterConsumption = mealPlan.Water,
                Notes = mealPlan.Notes,
                Meals = mealPlan.Meals?.Select(m => new MealDto()
                {
                    Id = m.Id,
                    GramsOfPortion = m.GramsOfPortion,
                    MealType = m.MealType.ToString(),
                    Dish = new DishDto()
                    {
                        Id = m.Dish.Id,
                        Name = m.Dish.Name,
                        DishProducts = m.Dish.DishProducts.Select(dp => new ProductDto()
                        {
                            Id = dp.Id,
                            Name = dp.Product.Name,
                            Brand = dp.Product.Brand,
                            Calories = dp.Product.Calories,
                            Carbohydrates = dp.Product.Carbohydrates,
                            Proteins = dp.Product.Proteins,
                            Ingredients = dp.Product.Ingredients,
                            GramsInPortion = dp.Product.GramsInPortion,
                            Fats = dp.Product.Fats,
                            Amount = dp.Amount,
                        }).ToList(),
                        DishApiProducts = m.Dish.DishApiProducts.Select(dap => new ApiProductDto()
                        {
                            ApiId = dap.ApiProductInfo.ApiId,
                            ApiUrl = dap.ApiProductInfo.ApiUrl,
                            Name = dap.ApiProductInfo.Name,
                            Brand = dap.ApiProductInfo.Brand,
                            Description = dap.ApiProductInfo.Description,
                            Portion = dap.ApiProductInfo.Portion,
                            Calories = dap.ApiProductInfo.Calories,
                            Carbohydrates = dap.ApiProductInfo.Carbohydrates,
                            Proteins = dap.ApiProductInfo.Proteins,
                            Fats = dap.ApiProductInfo.Fats,
                            GramsInPortion = dap.ApiProductInfo.GramsInPortion,
                            Amount = dap.Amount,
                        }).ToList(),
                    },
                }).ToList(),
                CaloriesTotal = mealPlan.Meals.Sum(m => m.Dish.Calories),
                CarbohydratesTotal = mealPlan.Meals.Sum(m => m.Dish.Carbohydrates),
                ProteinsTotal = mealPlan.Meals.Sum(m => m.Dish.Proteins),
                FatsTotal = mealPlan.Meals.Sum(m => m.Dish.Fats),
            };
        }

        public void AddToMealPlan(Guid mealPlanId, Guid dishId, uint gramsOfPortion, MealType mealType, string userId)
        {
            var firstOrDefault = _dbContext.DailyMealPlans
                .FirstOrDefault(mp => mp.Id == mealPlanId && mp.UserId == userId);

            if (firstOrDefault is null || firstOrDefault.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            var dish = _dbContext.Dishes
                .FirstOrDefault(d => d.Id == dishId);

            if (dish is null || dish.UserId != userId)
            {
                throw new ForbidException("User claims invalid");
            }

            var meal = new Meal
            {
                Id = Guid.NewGuid(),
                DailyMealPlanId = mealPlanId,
                DishId = dishId,
                MealType = mealType,
                GramsOfPortion = (int)gramsOfPortion,
            };

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();
        }

        private DailyMealPlan CreateMealPlan(DateTime date, string userId)
        {
            var mealPlan = new DailyMealPlan
            {
                Id = Guid.NewGuid(),
                Date = date,
                Water = 0,
                UserId = userId,
            };

            _dbContext.DailyMealPlans.Add(mealPlan);
            _dbContext.SaveChanges();

            return mealPlan;
        }
    }
}