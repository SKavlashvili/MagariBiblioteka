using Microsoft.Extensions.DependencyInjection;
using NpgSQLConnectionWrapper.Exceptions;
using NpgSQLConnectionWrapper.Middlewares;
using NpgSQLConnectionWrapper.UserTools;
using System.Reflection;

namespace NpgSQLConnectionWrapper
{
    public static class ServiceExtensions
    {
        public static void AddDBServices(this IServiceCollection collection,Assembly contextsAssembly, params Type[] dbContexts)
        {
            Type DbContextType = typeof(DbContext);
            for(int i = 0; i < dbContexts.Length; i++)
            {
                if (dbContexts[i].BaseType != DbContextType) throw new NotSupportedTypeException(dbContexts[i]);
                collection.AddScoped(dbContexts[i]);
            }
            ConnectionDestroyerMiddleware.SetContextAssembly(contextsAssembly);
        }
    }
}
