using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace poc_common.Configurations
{
    public class Configurations
    {
        public class Encrypt
        {
            public const string Section = "EncryptConfiguration";
            public string Salt { get; set; }
        }
        public string ConnectionString { get; set; }
        
    }
}
