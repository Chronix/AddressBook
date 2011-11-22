using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public IMType Type { get; set; }
        public string Account { get; set; }
    }
}
