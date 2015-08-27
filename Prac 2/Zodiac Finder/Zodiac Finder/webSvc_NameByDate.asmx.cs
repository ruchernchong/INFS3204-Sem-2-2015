using System;
using System.Diagnostics;
using System.Web.Services;

namespace Zodiac_Finder
{
    /// <summary>
    /// Summary description for webSvc_NameByDate
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webSvc_NameByDate : System.Web.Services.WebService
    {
        string[,] zodiac = 
            {
                {"Aquarius", "01", "21", "02", "19"},
                {"Pisces", "02", "20", "03", "20"},
                {"Aries", "03", "21", "04", "20"},
                {"Taurus", "04", "21", "05", "21"},
                {"Gemini", "05", "22", "06", "21"},
                {"Cancer", "06", "22", "07", "22"},
                {"Leo", "07", "23", "08", "22"},
                {"Virgo", "08", "23", "09", "23"},
                {"Libra", "09", "24", "10", "23"},
                {"Scorpio", "10", "24", "11", "22"},
                {"Sagittarius", "11", "23", "12", "21"},
                {"Capricorn", "12", "22", "01", "20"}
            };

        [WebMethod]
        public string FindZodiacByDate(int mth, int day)
        {
            DateTime getDateFromInput = new DateTime(1900, 01, 01);

            try
            {
                // Force Year to be 2012, a leap year.
                getDateFromInput = new DateTime(2012, mth, day);
            }
            catch (Exception ex)
            {
                 Debug.WriteLine(ex.Message);
            }

            for (int i = 0; i < zodiac.GetLength(0); i++)
            {
                DateTime dateLower = new DateTime(2012, int.Parse(zodiac[i, 1]), int.Parse(zodiac[i, 2]));
                DateTime dateUpper = new DateTime(2012, int.Parse(zodiac[i, 3]), int.Parse(zodiac[i, 4]));

                if (getDateFromInput <= new DateTime(2012, 01, 01))
                {
                    return "Invalid Input";
                }
                else if (getDateFromInput > new DateTime(2012, 12, 22) || getDateFromInput < new DateTime(2012, 01, 19))
                {
                    return "Capricorn";
                }
                else if (getDateFromInput >= dateLower && getDateFromInput <= dateUpper)
                {
                    Debug.WriteLine(zodiac[i, 0]);
                    return zodiac[i, 0];
                }
            }
            return "Wrong Input Date";
        }
    }
}