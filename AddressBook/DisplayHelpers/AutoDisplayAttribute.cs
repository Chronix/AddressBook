using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AddressBook.DisplayHelpers
{
    [AttributeUsage(AttributeTargets.Property)]
    public class AutoDisplayAttribute : Attribute
    {
        public bool AutoDisplay { get; private set; }
        public string DisplayName { get; private set; }

        public AutoDisplayAttribute(bool autoDisplay, string displayName)
        {
            AutoDisplay = autoDisplay;
            DisplayName = displayName;
        }

        public AutoDisplayAttribute(bool autoDisplay)
            : this(autoDisplay, null) { }
    }
}
