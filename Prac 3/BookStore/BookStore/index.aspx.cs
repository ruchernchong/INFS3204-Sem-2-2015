using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Text.RegularExpressions;
//using System.Web.UI;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace BookStore
{
    public partial class index : System.Web.UI.Page
    {
        BookStoreService BookStoreService = new BookStoreService();
        String[] errorMessages = {
                                   "Input is empty.",
                                   "Invalid input type."
                               };

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

            txtBookYear.MaxLength = 4;

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
                    loadAllBooks();
                }
                catch (Exception ex)
                {
                    DebuggerInfo.Text = "Debug Message: " + ex.Message.ToString();
                }
            }
        }

        protected void btnAddBooks_Click(object sender, EventArgs e)
        {
            DebuggerInfo.Text = null;

            String[] newBook = {
                                   txtBookID.Text,
                                   txtBookName.Text,
                                   txtBookAuthor.Text,
                                   txtBookYear.Text,
                                   txtBookPrice.Text,
                                   txtBookStock.Text
                               };

            Debug.WriteLine(newBook);
            BookStoreService.addBook(newBook);
        }

        protected void btnDeleteBooks_Click(object sender, EventArgs e)
        {
            DebuggerInfo.Text = null;

            string type = dropDelete.SelectedValue;
            string input = txtDelete.Text;

            if (String.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show(errorMessages[0]);
                DebuggerInfo.Text = errorMessages[0];
            } else {
                try
                {
                    BookStoreService.deleteBook(type, input);
                }
                catch (FaultException<Exception> faultEx)
                {
                    Debug.WriteLine(faultEx.Detail.Message);
                    MessageBox.Show(faultEx.Detail.Message);
                    DebuggerInfo.Text = faultEx.Detail.Message;
                }
                
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnSearchBooks_Click(object sender, EventArgs e)
        {
            DebuggerInfo.Text = null;

            string type = dropSearch.SelectedValue;
            string input = txtSearch.Text;

            Regex reg = new Regex(input, RegexOptions.IgnoreCase);
            Debug.WriteLine(reg.Matches(input));

            if (String.IsNullOrWhiteSpace(input))
            {
                DebuggerInfo.Text = errorMessages[0];
            }
            else
            {
                switch (type)
                {
                    case ("Name"):
                        try
                        {
                            dataGrid_FileOutput.DataSource = BookStoreService.searchBook(type, input);
                            dataGrid_FileOutput.DataBind();
                        }
                        catch (Exception ex)
                        {

                        }
                        break;
                    case ("ID"):
                        try
                        {
                            dataGrid_FileOutput.DataSource = BookStoreService.searchBook(type, input);
                            dataGrid_FileOutput.DataBind();
                        }
                        catch
                        {

                        }
                        break;
                    case ("Author"):
                        try
                        {
                            dataGrid_FileOutput.DataSource = BookStoreService.searchBook(type, input);
                            dataGrid_FileOutput.DataBind();
                        }
                        catch
                        {

                        }
                        break;

                    case ("Year"):
                        dataGrid_FileOutput.DataSource = BookStoreService.searchBook(type, input);
                        dataGrid_FileOutput.DataBind();

                        break;
                    default:
                        DebuggerInfo.Text = errorMessages[1];
                        break;
                }
            }
        }

        public void loadAllBooks()
        {
            var bookList = BookStoreService.GetAllBooks();
            Debug.WriteLine(bookList);

            dataGrid_FileOutput.DataSource = bookList;
            dataGrid_FileOutput.DataBind();
        }
    }
}