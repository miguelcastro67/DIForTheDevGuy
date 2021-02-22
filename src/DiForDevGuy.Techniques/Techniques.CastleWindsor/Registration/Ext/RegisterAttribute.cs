using System;

namespace Ext
{
    public class RegisterAttribute : Attribute
    {
        public RegisterAttribute()
        {
        }

        public RegisterAttribute(Type @for)
        {
            For = @for;
        }

        public Type For { get; set; }
    }
}
