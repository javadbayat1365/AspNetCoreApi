using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Settings
{
    public class IdentitySettings
    {

        public bool PasswordRequireDigit { get; set; }
        public int PasswordRequiredLength { get; set; }
        public bool PasswordRequireNonAlphanumeric { get; set; }
        public bool PasswordRequireLowercase { get; set; }
        public bool PasswordRequireUppercase { get; set; }
        public bool userRequireUniqueEmail { get; set; }
        public bool SignInRequireConfirmedPhoneNumber { get; set; }
        public short LockOutMaxFailedAccessAttempts { get; set; }
        //public TimeSpan LockOutDefaultLockoutTimeSpan { get; set; }
        public bool LockOutAllowedForNewUsers { get; set; }
    }
}
