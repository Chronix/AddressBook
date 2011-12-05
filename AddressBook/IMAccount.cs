using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace AddressBook
{
    public enum IMType
    {
        ICQ,
        MSN,
        Yahoo,
        AIM,
        Jabber,
        GTalk
    }

    [Serializable]
    public class IMAccount
    {
        [XmlAttribute("Type")]
        public IMType Type { get; set; }

        [XmlAttribute("Value")]
        public string Account { get; set; }
    }
}
