using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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

            var NumIDYear = new List<String> {
                "Num",
                "ID",
                "Year"
            };
            dropNumIDYear.DataSource = NumIDYear;
            dropNumIDYear.DataBind();

            var NumIDAuthorYear = new List<String> {
                "Num",
                "ID",
                "Author",
                "Year"
            };
            dropNumIDAuthorYear.DataSource = NumIDAuthorYear;
            dropNumIDAuthorYear.DataBind();

            BookStoreService bookStore = new BookStoreService();

            try
            {
                dataGrid_FileOutput.DataSource = bookStore.GetAllBooks();
                dataGrid_FileOutput.DataBind();

                Debug.WriteLine(bookStore.GetAllBooks());
            }
            catch (Exception ex)
            {
                DebuggerInfo.Text = "Debug Message: {0}" + ex.Message.ToString();
            }
        }
    }
}