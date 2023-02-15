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
            catch(Exception ex)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(String.Format("Something has gone wrong. Please see the error message: {0}", ex.Message));
            }
        }
    }
}
