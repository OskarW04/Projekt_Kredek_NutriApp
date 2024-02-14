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
                _logger.LogInformation(resourceNotFound, resourceNotFound.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(resourceNotFound.Message);
            }
            catch (BadHttpRequestException badHttpRequest)
            {
                _logger.LogInformation(badHttpRequest, badHttpRequest.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(badHttpRequest.Message);
            }
            catch (ResourceAlreadyExistsException resourceAlreadyExists)
            {
                _logger.LogInformation(resourceAlreadyExists, resourceAlreadyExists.Message);
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(resourceAlreadyExists.Message);
            }
            catch (ForbidException forbid)
            {
                _logger.LogInformation(forbid, forbid.Message);
                context.Response.StatusCode = 403;
                await context.Response.WriteAsJsonAsync(forbid.Message);
            }
            catch (FoodDatabaseApiErrorException foodDatabaseApiError)
            {
                _logger.LogInformation(foodDatabaseApiError, foodDatabaseApiError.Message);
                context.Response.StatusCode = 404;
                await context.Response.WriteAsJsonAsync(foodDatabaseApiError.Message);
            }
            catch (IncorrectInputTypeException incorrectInputType)
            {
                _logger.LogInformation(incorrectInputType, incorrectInputType.Message);
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