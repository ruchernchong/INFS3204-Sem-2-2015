using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;

namespace Zodiac_Finder
{
    /// <summary>
    /// Summary description for webSvc_PostcodeFinder
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class webSvc_PostcodeFinder : System.Web.Services.WebService
    {
        public class Suburb
        {
            public string dropSuburb { get; set; }
        }

        //[ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]

        [WebMethod]
        public string PostcodeFinder(string dropSuburb)
        {
            string file = "Postcodes.txt";
            string sourcePath = Directory.GetCurrentDirectory();

            string[] delimiters = {",",
                                      "\r\n"
                                  };
            string[] getSuburbs = File.ReadAllLines(sourcePath + "/" + file);
            string[][] arraySuburbs = getSuburbs.Select(suburbs => suburbs.Split(delimiters, StringSplitOptions.RemoveEmptyEntries).ToArray()).ToArray();
            Debug.WriteLine(arraySuburbs);

            for (int j = 0; j < arraySuburbs.GetLength(0); j++)
            {
                if (String.Compare(dropSuburb, arraySuburbs[j][0], true) == 0)
                {
                    return arraySuburbs[j][1];
                }
            }
            return "Invalid Selection";
        }
    }
}
