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
                {"08", "02", "Leo"},
                {"08", "30", "Virgo"},
                {"09", "25", "Libra"},
                {"11", "08", "Scorpio"},
                {"11", "30", "Sagittarius"},
                {"12", "25", "Capricorn"}
            };

        [WebMethod]
        public string FindZodiacByDate(int mon, int day)
        {
            for (int i = 0; i < zodiac.GetLength(0); i++)
            {
                if (int.Parse(zodiac[i, 0]) >= mon && int.Parse(zodiac[i, 1]) >= day)
                {
                    return zodiac[i, 2];
                }
            }
            return "Invalid Input";
        }
    }
}