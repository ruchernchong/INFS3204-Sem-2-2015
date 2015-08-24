using System;
using System.Diagnostics;
using System.Web.Services;

namespace Zodiac_Finder
{
    /// <summary>
    /// Summary description for webSvc_DateByName
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class webSvc_DateByName : System.Web.Services.WebService
    {
        string[,] zodiac = 
            {
                {"Aries", "03/21 - 04/20"},
                {"Taurus", "04/21 - 05/21"},
                {"Gemini", "05/22 - 06/21"},
                {"Cancer", "06/22 - 07/22"},
                {"Leo", "07/23 - 08/22"},
                {"Virgo", "08/23 - 09/23"},
                {"Libra", "09/24 - 10/23"},
                {"Scorpio", "10/24 - 11/22"},
                {"Sagittarius", "11/23 - 12/21"},
                {"Capricorn", "12/22 - 01/20"},
                {"Aquarius", "01/21 - 02/19"},
                {"Pisces", "02/20 - 03/20"}
            };

        [WebMethod]
        public string FindDateByZodiac(string dateInterval)
        {
            Debug.WriteLine(zodiac);
            for (int i = 0;  i < zodiac.GetLength(0); i++)
            {
                if (String.Compare(dateInterval, zodiac[i, 0], true) == 0)
                {
                    Debug.WriteLine(i);
                    return zodiac[i, 1];
                }
            }
            return "Date Interval not found.";
        }
    }
}
