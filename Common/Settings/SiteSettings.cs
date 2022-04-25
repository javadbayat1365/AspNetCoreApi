using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Settings
{
   public class SiteSettings
    {
        public string ElmahPath { get; set; }
        public JwtSettings  JwtSettings{ get; set; }
        public IdentitySettings  IdentitySettings{ get; set; }
    }
}
