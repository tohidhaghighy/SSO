using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;

namespace SSO.Infrastructure
{
    public class AuthenticationDbContext:DbContext
    {
        public AuthenticationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<ApplicationRediretUrl> ApplicationRediretUrls{ get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }

    }
}