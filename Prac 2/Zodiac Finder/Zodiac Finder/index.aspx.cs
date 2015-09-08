using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Zodiac_Finder.serviceRef_DateByName;
using Zodiac_Finder.serviceRef_NameByDate;
using Zodiac_Finder.serviceRef_PostcodeFinder;

namespace Zodiac_Finder
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    webSvc_PostcodeFinderSoapClient populateSuburb = new serviceRef_PostcodeFinder.webSvc_PostcodeFinderSoapClient();
                    dropSuburb.DataSource = populateSuburb.SuburbList();
                    dropSuburb.DataBind();
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"" + ex.Message + "\");</script>");
                }
            }
            
            lblDateByName_InputName.Text = "Name of Zodiac: ";
            lblDateByName_ResultName.Text = "Date Range: ";
            lblNameByDate_Month.Text = "Month: ";
            lblNameByDate_Day.Text = "Day: ";
            lblNameByDate_ResultName.Text = "Name of Zodiac: ";

            btnDateByName_GetDate.Text = "Get Date";
            btnDateByName_GetDate.UseSubmitBehavior = false;
            btnNameByDate_GetZodiac.Text = "Get Zodiac";
            btnNameByDate_GetZodiac.UseSubmitBehavior = false;
            //btnShowPostcode.Text = "Show Postcode";
            //btnShowPostcode.UseSubmitBehavior = false;

            txtResultDateByName_GetDateInterval.Enabled = false;
            txtResultNameByDate_GetName.Enabled = false;

            lblPostcode.Text = "Postcode: ";
            lblTimestamp.Text = DateTime.Now.ToString();

            
        }

        protected void btnDateByName_GetDate_Click(object sender, EventArgs e)
        {
            webSvc_DateByNameSoapClient GetDateInterval = new serviceRef_DateByName.webSvc_DateByNameSoapClient();
            txtResultDateByName_GetDateInterval.Text = String.Format("{0}",
                GetDateInterval.FindDateByZodiac(txtDateByName_Name.Text).ToString());
        }

        protected void btnNameByDate_GetZodiac_Click(object sender, EventArgs e)
        {
            webSvc_NameByDateSoapClient GetZodiacName = new serviceRef_NameByDate.webSvc_NameByDateSoapClient();

            int valTxtNameByDate_Month = 0, valTxtNameByDate_Day = 0;

            try
            {
                valTxtNameByDate_Month = int.Parse(txtNameByDate_Month.Text);
                valTxtNameByDate_Day = int.Parse(txtNameByDate_Day.Text);
            }
            catch (Exception q)
            {
                Response.Write("<script>alert(\"" + q.Message + "\");</script>");
                Debug.WriteLine(q.Message);
            }
            finally
            {
                txtResultNameByDate_GetName.Text = String.Format("{0}",
                    GetZodiacName.FindZodiacByDate(valTxtNameByDate_Month, valTxtNameByDate_Day).ToString());
            }
        }
    }
}