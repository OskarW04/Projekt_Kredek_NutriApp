namespace NutriApp.Server.Models.MealPlan
{
    public class MealPlanDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string? Notes { get; set; }
        public int WaterConsumption { get; set; }
        public List<MealDto>? Meals { get; set; }

        public int CaloriesTotal { get; set; }
        public int CarbohydratesTotal { get; set; }
        public int ProteinsTotal { get; set; }
        public int FatsTotal { get; set; }
    }
}