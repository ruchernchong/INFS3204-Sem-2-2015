using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Zodiac_Finder.FindDateByName;
using Zodiac_Finder.FindNameByDate;
using Zodiac_Finder.PostcodeFinder;

namespace Zodiac_Finder
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string file = "Postcodes.txt";
            string sourcePath = Directory.GetCurrentDirectory();

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

            if (!IsPostBack)
            {
                try
                {
                    var suburb = new List<string>();
                    StreamReader readerPostcodes = new StreamReader(sourcePath + "/" + file);
                    string getSuburbs;
                    while ((getSuburbs = readerPostcodes.ReadLine()) != null)
                    {
                        string[] suburbs = getSuburbs.Split(',');

                        Debug.WriteLine(suburbs[0]);

                        dropSuburb.Items.Add(suburbs[0]);
                    }

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert(\"" + ex.Message + "\");</script>");
                }
            }
        }

        protected void btnDateByName_GetDate_Click(object sender, EventArgs e)
        {
            webSvc_DateByName GetDateInterval = new webSvc_DateByName();
            txtResultDateByName_GetDateInterval.Text = String.Format("{0}",
                GetDateInterval.FindDateByZodiac(txtDateByName_Name.Text).ToString());
        }

        protected void btnNameByDate_GetZodiac_Click(object sender, EventArgs e)
        {
            webSvc_NameByDate GetZodiacName = new webSvc_NameByDate();

            int valTxtNameByDate_Month = 0, valTxtNameByDate_Day = 0;

            try
            {
                valTxtNameByDate_Month = int.Parse(txtNameByDate_Month.Text);
                valTxtNameByDate_Day = int.Parse(txtNameByDate_Day.Text);
            }
            catch (Exception q)
            {
                Response.Write("<script>alert(\"" + q.Message + "\");</script>");
            }
            finally
            {
                txtResultNameByDate_GetName.Text = string.Format("{0}",
                    GetZodiacName.FindZodiacByDate(valTxtNameByDate_Month, valTxtNameByDate_Day).ToString());
            }
        }

        protected void btnShowPostcode_Click(object sender, EventArgs e)
        {
            webSvc_PostcodeFinder PostcodeFinder = new webSvc_PostcodeFinder();
            lblPostcode.Text = string.Format("Postcode: {0}",
                PostcodeFinder.PostcodeFinder(dropSuburb.SelectedItem.Value).ToString());
        }
    }
}