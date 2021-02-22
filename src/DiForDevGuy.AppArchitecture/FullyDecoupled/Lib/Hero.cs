using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lib
{
    [DataContract]
    public class Hero
    {
        public Hero(string superheroName, string realName, string power)
        {
            SuperheroName = superheroName;
            RealName = realName;
            Power = power;
        }

        [DataMember]
        public string SuperheroName { get; set; }
        [DataMember]
        public string RealName { get; set; }
        [DataMember]
        public string Power { get; set; }
    }
}
