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
            catch(Task_Type_Not_Provided_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}",errorMessage, ex.Message));
            }
            catch(User_Not_Found_Exception ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Task_Not_Found_Exception ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Wrong_Credentials_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(User_Already_Exists_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Password_Does_Not_Meet_Rules_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Not_Email_Format_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(User_Not_Allowed_Exception ex)
            {
                context.Response.StatusCode = 403;
                await context.Response.WriteAsync(String.Format("{0}: {1}", errorMessage, ex.Message));
            }
            catch(Refresh_Token_Invalid_Exception ex)
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
