using ToDoBackend.Exceptions;

namespace ToDoBackend
{
    public class ExceptionHandlingMiddleware :IMiddleware
    {
        //private readonly RequestDelegate _next;
        //public ExceptionHandlingMiddleware(RequestDelegate next)
        //{
        //    _next = next;
        //}

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(Task_Type_Not_Provided_Exception ex)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(String.Format("Something has gone wrong. Please see the error message: {0}", ex.Message));
            }
            catch(User_Not_Found_Exception ex)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(String.Format("Something has gone wrong. Please see the error message: {0}", ex.Message));
            }
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(String.Format("Something has gone wrong. Please see the error message: {0}", ex.Message));
            }
        }
    }
}
