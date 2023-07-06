using Microsoft.AspNetCore.Builder;
using NpgSQLConnectionWrapper.Middlewares;

namespace NpgSQLConnectionWrapper
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseConnectionCutter(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ConnectionDestroyerMiddleware>();
        }
    }
}
