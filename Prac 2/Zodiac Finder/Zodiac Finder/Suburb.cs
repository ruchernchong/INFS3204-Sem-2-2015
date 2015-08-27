using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zodiac_Finder
{
    public class Suburb
    {
        private string itemSuburb;
        private string itemPostcode;

        public string SuburbName
        {
            get
            {
                return itemSuburb;
            }
            set
            {
                itemSuburb = value;
            }
        }

        public string Postcode
        {
            get
            {
                return itemPostcode;
            }
            set
            {
                itemPostcode = value;
            }
        }
    }

    public class listSuburb
    {
        public List<Suburb> listSuburbs
        {
            get;
            set;
        }
    }
}