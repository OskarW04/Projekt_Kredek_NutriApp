namespace NutriApp.Server.Exceptions
{
    public class FoodDatabaseApiErrorException : Exception
    {
        public FoodDatabaseApiErrorException(string message) : base(message)
        {
        }
    }
}