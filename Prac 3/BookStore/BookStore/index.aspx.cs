using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace BookStore
{
    public partial class index : System.Web.UI.Page
    {
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
            }

            BookStoreService BookStoreService = new BookStoreService();

            try
            {
                dataGrid_FileOutput.DataSource = BookStoreService.GetAllBooks();
                dataGrid_FileOutput.DataBind();
            }
            catch (Exception ex)
            {
                DebuggerInfo.Text = "Debug Message: {0}" + ex.Message.ToString();
            }
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
                dataGrid_FileOutput.DataSource = null;
                dataGrid_FileOutput.DataBind();
                DebuggerInfo.Text = "Input is empty";
            }
            else
            {
                BookStoreService BookStoreService = new BookStoreService();

                switch (dropSearch.SelectedValue)
                {
                    case ("Name"):
                        try
                        {
                            
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
                        try
                        {
                            int year;
                            year = int.Parse(txtSearch.Text);

                            if (year > 0)
                            {
                                dataGrid_FileOutput.DataSource = BookStoreService.searchBook("", "", "", year);
                                dataGrid_FileOutput.DataBind();
                            }
                            else if (BookStoreService.GetAllBooks().Count == 0)
                            {
                                MessageBox.Show("No results found.");
                                DebuggerInfo.Text = "No results found.";
                            }
                            else
                            {
                                dataGrid_FileOutput.DataSource = null;
                                dataGrid_FileOutput.DataBind();
                                DebuggerInfo.Text = "Please make sure that Year is a positive integer.";
                            }
                        }
                        catch (Exception exceptionYear)
                        {
                            //MessageBox.Show(exceptionYear.Message.ToString());
                            DebuggerInfo.Text = exceptionYear.Message.ToString();
                        }
                        break;
                    default:
                        DebuggerInfo.Text = "Invalid input selected";
                        break;
                }
            }
        }
    }
}