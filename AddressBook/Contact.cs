using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AddressBook.DisplayHelpers;

namespace AddressBook
{
    [Serializable]
    public class Contact
    {
        [AutoDisplay(true, "First Name")]
        public string FirstName { get; set; }

        [AutoDisplay(true, "Middle Name")]
        public string MiddleName { get; set; }

        [AutoDisplay(true, "Last Name")]
        public string LastName { get; set; }

        [AutoDisplay(true, "Initials")]
        public string Initials { get; set; }

        [AutoDisplay(true, "Address Line 1")]
        public string AddressLine1 { get; set; }

        [AutoDisplay(true, "Address Line 2")]
        public string AddressLine2 { get; set; }

        [AutoDisplay(true, "City")]
        public string City { get; set; }

        [AutoDisplay(true, "ZIP")]
        public string ZIP { get; set; }

        [AutoDisplay(true, "Country")]
        public string Country { get; set; }

        [AutoDisplay(true, "Home Phone")]
        public string HomePhone { get; set; }

        [AutoDisplay(true, "Work Phone")]
        public string WorkPhone { get; set; }

        [AutoDisplay(true, "Cell Phone")]
        public string CellPhone { get; set; }

        [AutoDisplay(true, "Company")]
        public string Company { get; set; }

        [AutoDisplay(true, "Job Title")]
        public string JobTitle { get; set; }

        [AutoDisplay(true, "E-Mail")]
        public string EMail { get; set; }
        
        [AutoDisplay(true, "Home Page")]
        public string HomePage { get; set; }

        [AutoDisplay(false)]
        public string ImagePath { get; set; }

        public RefreshableCollection<IMAccount> IMAccounts { get; set; }

        public Contact()
        {
            IMAccounts = new RefreshableCollection<IMAccount>();
        }

        public override string ToString()
        {
            return FirstName + " " + LastName;
        }

        public bool HasIMContact(IMType type)
        {
            var wanted = IMAccounts.Where(acc => acc.Type == type);

            if (wanted.Count() > 0)
            {
                wanted = wanted.Where(acc => !string.IsNullOrWhiteSpace(acc.Account));

                return wanted.Count() > 0;
            }
            else return false;
        }
    }
}
