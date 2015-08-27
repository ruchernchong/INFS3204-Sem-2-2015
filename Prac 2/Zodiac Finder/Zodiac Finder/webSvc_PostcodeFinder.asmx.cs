using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        [ScriptMethod(ResponseFormat = ResponseFormat.Json, UseHttpGet = false)]

        [WebMethod]
        public List<string> SuburbList()
        {
            string[] readerSuburb = ReadLines().ToArray();
            Debug.WriteLine(readerSuburb);

            Dictionary<string, string> Suburbs = new Dictionary<string, string>();

            string[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i =0; i<readerSuburb.GetLength(0); i++)
            {

                string[] arraySuburbs = readerSuburb[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Debug.WriteLine(arraySuburbs);
                Suburbs.Add(arraySuburbs[0], arraySuburbs[1]);
                Debug.WriteLine("Count: {0}", Suburbs.Count);
            }

            List<string> listSuburb = new List<string>();

            foreach (string suburb in Suburbs.Keys)
            {
                listSuburb.Add(suburb);
                Debug.WriteLine(suburb);
            }
            return listSuburb;
        }

        [WebMethod]
        public List<string> PostcodeList()
        {
            string[] readerPostcode = ReadLines().ToArray();

            Dictionary<string, string> Postcodes = new Dictionary<string, string>();

            string[] delimiters = {
                                      ",",
                                      "\r\n"
                                  };

            for (int i = 0; i < readerPostcode.GetLength(0); i++)
            {
                string[] arrayPostcodes = readerPostcode[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                Postcodes.Add(arrayPostcodes[0], arrayPostcodes[1]);
                Debug.WriteLine("Count: {0}", Postcodes.Count);
            }

            List<string> listPostcode = new List<string>();

            foreach (string postcode in Postcodes.Values)
            {
                listPostcode.Add(postcode);
                Debug.WriteLine(postcode);
            }
            return listPostcode;
        }

        [WebMethod]
        public string PostcodeFinder(string dropSuburb)
        {
            string[] arraySuburbs = SuburbList().ToArray();
            string[] arrayPostcodes = PostcodeList().ToArray();

            List<String> finalList = new List<String>(arraySuburbs.Concat<String>(arrayPostcodes));
            string[] finalArray = finalList.ToArray();

            Debug.WriteLine(finalArray);

            for (int i = 0; i < arrayPostcodes.GetLength(0); i++)
            {
                if (String.Compare(dropSuburb, finalArray[i], true) == 0)
                {
                    return arrayPostcodes[i];
                }
            }
            return "Invalid Selection";
        }

        public IEnumerable<String> ReadLines()
        {
            string file = "/Postcodes.txt";
            string sourcePath = Directory.GetCurrentDirectory();

            StreamReader readerSuburbPostcode = new StreamReader(sourcePath + file);

            string line;
            while ((line = readerSuburbPostcode.ReadLine()) != null)
            {
                yield return line;
                Debug.WriteLine(line);
            }
        }
    }
}