using Microsoft.AspNetCore.Http;
using NpgSQLConnectionWrapper.UserTools;
using System.Reflection;

namespace NpgSQLConnectionWrapper.Middlewares
{
    public class ConnectionDestroyerMiddleware
    {
        private readonly RequestDelegate _next;

        private static List<Type> _dbContextTypes;
        private static Assembly ContextsAssembly;

        public static void SetContextAssembly(Assembly assembly)
        {
            ContextsAssembly = assembly;
            Type dbContextType = typeof(DbContext);
            _dbContextTypes = ContextsAssembly.GetTypes().Where(type => type.BaseType == dbContextType).ToList();
        }

        public ConnectionDestroyerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
                DbContext specificContext;
                for (int i = 0; i < _dbContextTypes.Count; i++)
                {
                    specificContext = (DbContext)context.RequestServices.GetService(_dbContextTypes[i]);
                    if (specificContext.ConnectionEstablished)
                    {
                        specificContext.Dispose();
                        Console.WriteLine("Connection destroyed");//Delete
                    }
                }
            }catch(Exception ex)
            {
                DbContext specificContext;
                for (int i = 0; i < _dbContextTypes.Count; i++)
                {
                    specificContext = (DbContext)context.RequestServices.GetService(_dbContextTypes[i]);
                    if (specificContext.ConnectionEstablished)
                    {
                        specificContext.Dispose();
                        Console.WriteLine("Connection destroyed");//Delete
                    }
                }
                throw ex;
            }
        }
    }
}
