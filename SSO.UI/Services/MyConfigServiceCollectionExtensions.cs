using Authentication_Server.Application.Services.Access;
using Authentication_Server.Application.Services.Application;
using Authentication_Server.Application.Services.Role;
using Authentication_Server.Application.Services.User;
using Authentication_Server.Core.Contracts.Access;
using Authentication_Server.Core.Contracts.Application;
using Authentication_Server.Core.Contracts.Role;
using Authentication_Server.Core.Contracts.User;

namespace Authentication_Server.UI.Services
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddConfig(
             this IServiceCollection services, IConfiguration config)
        {
            return services;
        }

        public static IServiceCollection AddMyDependencyGroup(
             this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccessService, AccessService>();
            services.AddScoped<IRoleAccessService, RoleAccessService>();
            services.AddScoped<IUserAccessService, UserAccessService>();

            return services;
        }
    }
}
