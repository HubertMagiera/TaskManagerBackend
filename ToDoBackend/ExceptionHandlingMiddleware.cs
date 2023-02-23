using ToDoBackend.Exceptions;

namespace ToDoBackend
{   
    //class used for handling errors
    public class ExceptionHandlingMiddleware :IMiddleware
    {
        //standard error message
        private readonly string errorMessage = "Something has gone wrong. Please see the error message:";
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(TaskTypeNotProvidedException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}",errorMessage, ex.Message));
            }
            catch(UserNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(TaskNotFoundException ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(WrongCredentialsException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(UserAlreadyExistsException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(PasswordDoesNotMeetRulesException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(NotEmailFormatException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(UserNotAllowedException ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(RefreshTokenInvalidException ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
        }
    }
}
