using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Settings
{
   public class JwtSettings
    {
        public string SecretKey { get; set; }
        public string EncriptKey { get; set; }
        public string Issure { get; set; }
        public string Audience { get; set; }
        public int NotBeforMinutes { get; set; }
        public int ExpirationMinutes { get; set; }
        public int ClockSkew { get; set; }
    }
}
