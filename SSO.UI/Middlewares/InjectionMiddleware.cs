namespace Authentication_Server.UI.Middlewares
{
    public class InjectionMiddleware
    {
        private readonly RequestDelegate _next;

        public InjectionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {

            return _next(httpContext);
        }
    }
}
