using Microsoft.EntityFrameworkCore;
using SSO.Core.Models;

namespace SSO.Infrastructure
{
    public class AuthenticationDbContext:DbContext
    {
        public AuthenticationDbContext(DbContextOptions<AuthenticationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>().HasData(
               new Application
               {
                   Id=1,
                   UpdateDate = DateTime.Now,
                   GeneratedKey="123456789",
                   InsertUserName="admin",
                   InsertDate=DateTime.Now,
                   Name="سامانه SSO",
                   InnerLink="",
                   Icon=""
               },
               new Application
               {
                   Id=2,
                   UpdateDate = DateTime.Now,
                   GeneratedKey = "1234",
                   InsertUserName = "admin",
                   InsertDate = DateTime.Now,
                   Name = "سامانه تیکتینگ ویرا",
                   InnerLink = "",
                   Icon = ""
               }
           );
            modelBuilder.Entity<Role>().HasData(
               new Role
               {
                   Id = 1,
                   Name = "ادمین سامانه SSO",
                   ApplicationId = 1,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "https://localhost:7062/Home/Index"
               },
               new Role
               {
                   Id = 2,
                   Name = "ادمین سامانه مدیریت پرونده ها",
                   ApplicationId=2,
                   InsertDate = DateTime.Now,
                   InsertUserName="admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel= "http://localhost:3000/api"
               },
               new Role
               {
                   Id = 3,
                   Name = "ادمین سامانه میز خدمت",
                   ApplicationId = 2,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "http://localhost:3000/api"
               },
               new Role
               {
                   Id = 4,
                   Name = "ادمین تعزیرات",
                   ApplicationId = 2,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "http://localhost:3000/api"
               },
               new Role
               {
                   Id = 5,
                   Name = "ادمین ویرا",
                   ApplicationId = 2,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "http://localhost:3000/api"
               },
               new Role
               {
                   Id = 6,
                   Name = "ادمین سامانه امحا",
                   ApplicationId = 2,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "http://localhost:3000/api"
               },
               new Role
               {
                   Id = 7,
                   Name = "ادمین سامانه تبادل اطلاعات",
                   ApplicationId = 2,
                   InsertDate = DateTime.Now,
                   InsertUserName = "admin",
                   UpdateDate = DateTime.Now,
                   UrlPanel = "http://localhost:3000/api"
               }
           );

            modelBuilder.Entity<User>().HasData(
               new User
               {
                   Id = 1,
                   UpdateDate = DateTime.Now,
                   InsertUserName = "admin",
                   InsertDate = DateTime.Now,
                   Name = "superuser",
                   Active = true,
                   ApplicationId= 1,
                   Email="",
                   Family= "superuser",
                   Mobile="",
                   Image="",
                   RoleId=1,
                   Password= "aosxGe1VJ8FIz3wnfJTdYnV1sl/swHqF7NbWdmdgkIc=",
                   RefreshToken = "t6UHmvVLRkg2d0OsvTR0gdkAY77slk59Ryww0SvsBTc=",
                   RefreshTokenExpiryTime= DateTime.Now.AddYears(3),
                   Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJVc2VySWQiOiIxNCIsIlVzZXJuYW1lIjoic3VwZXJ1c2VyIiwiVXNlclJvbGUiOiIxMCIsImV4cCI6MTcyNTIwMzQyNiwiaXNzIjoiaHR0cDovL2xvY2FsaG9zdDoyODc0Ny8iLCJhdWQiOiJodHRwOi8vbG9jYWxob3N0OjI4NzQ3LyJ9.RYvoJhqSnZHcLaoKKXotdms4dJBNGRazEaqoYONdMAQ",
                   UserName="superUser",
               }
           );
        }

        public DbSet<Application> Applications { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Access> Accesses { get; set; }
        public DbSet<ApplicationRediretUrl> ApplicationRediretUrls{ get; set; }
        public DbSet<UserAccess> UserAccesses { get; set; }
        public DbSet<RoleAccess> RoleAccesses { get; set; }
        public DbSet<UserAddress> UserAddresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Province> Provinces { get; set; }

    }
}