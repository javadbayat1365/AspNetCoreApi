using Common.Settings;
using Data;
using Entities.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
    public static class IdentityCollectionsExtensions
    {
        public static void AddCustomIdentity(this IServiceCollection services, IdentitySettings Settings)
        {

            services.AddIdentity<User, Role>(IdentityOptions =>
         {

             #region Password Setting

             IdentityOptions.Password.RequireDigit = Settings.PasswordRequireDigit;
             IdentityOptions.Password.RequiredLength = Settings.PasswordRequiredLength;
             IdentityOptions.Password.RequireNonAlphanumeric = Settings.PasswordRequireNonAlphanumeric;
             IdentityOptions.Password.RequireLowercase = Settings.PasswordRequireLowercase;
             IdentityOptions.Password.RequireUppercase = Settings.PasswordRequireUppercase;
             #endregion

             #region User Setting
             IdentityOptions.User.RequireUniqueEmail = Settings.userRequireUniqueEmail;
             #endregion

             #region SignIn Setting
             IdentityOptions.SignIn.RequireConfirmedPhoneNumber = Settings.SignInRequireConfirmedPhoneNumber;
             #endregion

             #region LockOut Setting
             //IdentityOptions.Lockout.MaxFailedAccessAttempts = Settings.LockOutMaxFailedAccessAttempts;
             //IdentityOptions.Lockout.DefaultLockoutTimeSpan = Settings.LockOutDefaultLockoutTimeSpan;
             //IdentityOptions.Lockout.AllowedForNewUsers = Settings.LockOutAllowedForNewUsers;
             #endregion

         }).AddEntityFrameworkStores<ApplicationDbContext>()
         .AddDefaultTokenProviders();
        }
    }
}
