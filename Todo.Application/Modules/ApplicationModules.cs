using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application.Services;
using Todo.Infrastructure.Modules;

namespace Todo.Application.Modules
{
    public static class ApplicationModules
    {
        public static IServiceCollection AllApplicationMudules(
            this IServiceCollection services)
        {
            services.AddInfrastructureModule();
            services.AddScoped<ITodoService, TodoService>();
            return services;
        }
    }
}
