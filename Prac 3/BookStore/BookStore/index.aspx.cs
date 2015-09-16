using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;

namespace BookStore
{
    public partial class index : System.Web.UI.Page
    {
        BookStoreService BookStoreService = new BookStoreService();

        protected void Page_Load(object sender, EventArgs e)
        {
            btnAddBooks.Text = "Add Books";
            btnDeleteBooks.Text = "Delete Books";
            btnSearchBooks.Text = "Search Books";

            lblBookID.Text = "ID: ";
            lblBookName.Text = "Name: ";
            lblBookAuthor.Text = "Author: ";
            lblBookYear.Text = "Year: ";
            lblBookPrice.Text = "Price: ";
            lblBookStock.Text = "Stock: ";

            btnAddBooks.UseSubmitBehavior = false;
            btnDeleteBooks.UseSubmitBehavior = false;
            btnSearchBooks.UseSubmitBehavior = false;

            if (!IsPostBack)
            {
                List<ListItem> dropDeleteItems = new List<ListItem>();

                dropDeleteItems.Add(new ListItem("Name"));
                dropDeleteItems.Add(new ListItem("ID"));
                dropDeleteItems.Add(new ListItem("Year"));

                dropDelete.Items.AddRange(dropDeleteItems.ToArray());

                List<ListItem> dropSearchItems = new List<ListItem>();

                dropSearchItems.Add(new ListItem("Name"));
                dropSearchItems.Add(new ListItem("ID"));
                dropSearchItems.Add(new ListItem("Author"));
                dropSearchItems.Add(new ListItem("Year"));

                dropSearch.Items.AddRange(dropSearchItems.ToArray());

                try
                {
                    GridView1.DataSource = BookStoreService.GetAllBooks();
                    GridView1.DataBind();
                    //dataGrid_FileOutput.DataSource = BookStoreService.GetAllBooks();
                    //dataGrid_FileOutput.DataBind();
                }
                catch (Exception ex)
                {
                    DebuggerInfo.Text = "Debug Message: {0}" + ex.Message.ToString();
                }
            }
        }


        public override void VerifyRenderingInServerForm(Control control)
        {
            //Required to verify that the control is rendered properly on page
        }

        protected void btnAddBooks_Click(object sender, EventArgs e)
        {

        }

        protected void btnDeleteBooks_Click(object sender, EventArgs e)
        {

        }

        protected void btnSearchBooks_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(txtSearch.Text))
            {
                DebuggerInfo.Text = "Input is empty";
            }
            else
            {
                switch (dropSearch.SelectedValue)
                {
                    case ("Name"):
                        try
                        {
                            string name;

                            try
                            {

                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        catch
                        {

                        }
                        break;
                    case ("ID"):
                        try
                        {

                        }
                        catch
                        {

                        }
                        break;
                    case ("Author"):
                        try
                        {

                        }
                        catch
                        {

                        }
                        break;

                    case ("Year"):
                        int year = 0;

                        try
                        {
                            year = int.Parse(txtSearch.Text);
                        }
                        catch (Exception exceptionYear)
                        {
                            DebuggerInfo.Text = exceptionYear.Message.ToString();
                        }
                        //dataGrid_FileOutput.DataSource = BookStoreService.searchBook(year);
                        //dataGrid_FileOutput.DataBind();
                        GridView1.DataSource = BookStoreService.searchBook(year);
                        GridView1.DataBind();

                        //Debug.WriteLine(year);
                        //Debug.WriteLine(BookStoreService.searchBook(year));

                        break;
                    default:
                        DebuggerInfo.Text = "Invalid input selected";
                        break;
                }
            }
        }
    }
}