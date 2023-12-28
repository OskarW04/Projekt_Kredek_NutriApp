namespace NutriApp.Server.Exceptions
{
    public class ResourceAlreadyExistsException : Exception
    {
        public ResourceAlreadyExistsException(string message) : base(message)
        {
        }
    }
}