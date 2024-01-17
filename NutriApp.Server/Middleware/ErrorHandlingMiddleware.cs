using NutriApp.Server.Exceptions;

namespace NutriApp.Server.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ErrorHandlingMiddleware> _logger;

        public ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (ResourceNotFoundException resourceNotFound)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(resourceNotFound.Message);
            }
            catch (BadHttpRequestException badHttpRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(badHttpRequest.Message);
            }
            catch (ResourceAlreadyExistsException resourceAlreadyExists)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(resourceAlreadyExists.Message);
            }
            catch (ForbidException forbid)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(forbid.Message);
            }
            catch (FoodDatabaseApiErrorException foodDatabaseApiError)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(foodDatabaseApiError.Message);
            }
            catch (IncorrectInputTypeException incorrectInputType)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(incorrectInputType.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsJsonAsync("Something went wrong...");
            }
        }
    }
}