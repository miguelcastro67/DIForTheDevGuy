using System;
using System.Collections.Generic;
using System.Text;

namespace Ext
{
    public class RegisterAttribute : Attribute
    {
        public RegisterAttribute()
        {
        }

        public RegisterAttribute(Type @as)
        {
            As = @as;
        }

        public Type As { get; set; }
    }
}
