using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.Windows.Forms;

namespace BookStore
{
    public partial class index : System.Web.UI.Page
    {
        //Declare BookStoreService from WCF Client.
        BookStoreService BookStoreService = new BookStoreService();
        //BookPurchaseService BookPurchaseService = new BookPurchaseService();
        BookPurchaseServiceReference.BookPurchaseServiceClient BookPurchaseService = new BookPurchaseServiceReference.BookPurchaseServiceClient();

        //Declare error messages to use in array form.
        String[] errorMessages = {
                                   "Input is empty. ",
                                   "Invalid input type. ",
                                   "No results found for "
                               };

        private static int moreBtnCount;

        protected void Page_Load(object sender, EventArgs e)
        {
            //Assigning values to ASP elements on Load.
            btnAddBooks.Text = "Add Books";
            btnDeleteBooks.Text = "Delete Books";
            btnSearchBooks.Text = "Search Books";
            btnMore.Text = "More";
            btnPurchase.Text = "Purchase";

            lblBookID.Text = "ID: ";
            lblBookName.Text = "Name: ";
            lblBookAuthor.Text = "Author: ";
            lblBookYear.Text = "Year: ";
            lblBookPrice.Text = "Price($): ";
            lblBookStock.Text = "Stock: ";
            lblTotalBudget.Text = "Total Budget: ";
            lblBookNo.Text = "Book No: ";
            lblQuantity.Text = "Amt: ";

            btnAddBooks.UseSubmitBehavior = false;
            btnDeleteBooks.UseSubmitBehavior = false;
            btnSearchBooks.UseSubmitBehavior = false;
            btnMore.UseSubmitBehavior = false;
            btnPurchase.UseSubmitBehavior = false;

            txtBookYear.MaxLength = 4;
            txtPurchase.Enabled = false;

            divEmptyResults.Visible = false;
            divErrorMessage.Visible = false;

            //Check if PostBack
            if (!IsPostBack)
            {
                //Populate DropDownList with items on Page_Load for dropDelete.
                List<ListItem> dropDeleteItems = new List<ListItem>();
                dropDeleteItems.Add(new ListItem("Name"));
                dropDeleteItems.Add(new ListItem("ID"));
                dropDeleteItems.Add(new ListItem("Year"));
                dropDelete.Items.AddRange(dropDeleteItems.ToArray());

                //Populate DropDownList with items on Page_Load for dropSearch.
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
                catch (Exception Ex)
                {
                    divErrorMessage.Visible = true;
                    divErrorMessage.Controls.Add(new LiteralControl(Ex.Message));
                }

                //this.NumberOfLblBookNumber = 0;
                //this.NumberOfLblQuantity = 0;

                this.NumberOfTxtBookNumber = 0;
                this.NumberOfTxtQuantity = 0;
            }
            else
            {
                this.CreateDynamicElements();
            }
        }

        protected void btnAddBooks_Click(object sender, EventArgs e)
        {
            try
            {
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
            catch (Exception Ex)
            {
                divErrorMessage.Visible = true;
                divErrorMessage.Controls.Add(new LiteralControl(Ex.Message));
            }

            Response.Redirect(Request.RawUrl);
        }

        protected void btnDeleteBooks_Click(object sender, EventArgs e)
        {
            string type = dropDelete.SelectedValue;
            string input = txtDelete.Text;

            if (String.IsNullOrWhiteSpace(input))
            {
                divErrorMessage.Visible = true;
                divErrorMessage.Controls.Add(new LiteralControl(errorMessages[0] + "Unable to delete " + input));
            }
            else
            {
                try
                {
                    BookStoreService.deleteBook(type, input);
                }
                catch (FaultException<Exception> faultEx)
                {
                    Debug.WriteLine(faultEx.Detail.Message);
                    divErrorMessage.Visible = true;
                    divErrorMessage.Controls.Add(new LiteralControl(faultEx.Message));
                }

                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnSearchBooks_Click(object sender, EventArgs e)
        {
            string type = dropSearch.SelectedValue;
            string input = txtSearch.Text;

            if (String.IsNullOrWhiteSpace(input))
            {
                divErrorMessage.Visible = true;
                divErrorMessage.Controls.Add(new LiteralControl(errorMessages[0] + " Unable to search " + input));
            }
            else
            {
                
                try
                {
                    if (BookStoreService.searchBook(type, input).Count > 0) {
                        dataGrid_DisplayData.DataSource = BookStoreService.searchBook(type, input);
                        dataGrid_DisplayData.DataBind();
                    }
                    else
                    {
                        dataGrid_DisplayData.Visible = false;

                        divEmptyResults.Visible = true;
                        divEmptyResults.Controls.Add(new LiteralControl(errorMessages[2] + input));
                    }
                }
                catch (FaultException<Exception> faultEx)
                {
                    divErrorMessage.Visible = true;
                    divErrorMessage.Controls.Add(new LiteralControl(faultEx.Detail.Message));
                }
            }
        }

        // Method to loadAllBooks(); Used for populating on Page_Load.
        public void loadAllBooks()
        {
            ICollection bookList = BookStoreService.GetAllBooks();
            Debug.WriteLine(bookList);

            dataGrid_DisplayData.DataSource = bookList;
            dataGrid_DisplayData.DataBind();
        }

        protected void btnMore_Click(object sender, EventArgs e)
        {
            //Label lblBookNumber = new Label();
            //Label lblQuantity = new Label();

            TextBox txtBookNumber = new TextBox(); 
            TextBox txtQuantity = new TextBox();

            LiteralControl lineBreak = new LiteralControl("<br />");

            //lblBookNumber.ID = "lblBookNumber_" + NumberOfLblBookNumber.ToString();
            //lblBookNumber.Text = "Book No: ";
            //lblBookNumber.CssClass = "control-label";
            //placeholderLblBookNumber.Controls.Add(lblBookNumber);
            //this.NumberOfLblBookNumber++;

            //lblQuantity.ID = "lblQuantity_" + NumberOfLblQuantity.ToString();
            //lblQuantity.Text = "Amt: ";
            //lblQuantity.CssClass = "control-label";
            //placeholderLblQuantity.Controls.Add(lblQuantity);
            //this.NumberOfLblQuantity++;

            txtBookNumber.ID = "txtBookNumber_" + NumberOfTxtBookNumber.ToString();
            txtBookNumber.CssClass = "form-control";

            txtQuantity.ID = "txtQuantity_" + NumberOfTxtQuantity.ToString();
            txtQuantity.CssClass = "form-control";

            //placeholderTxtBookNumber.Controls.Add(lineBreak);
            //placeholderTxtQuantity.Controls.Add(lineBreak);

            placeholderTxtBookNumber.Controls.Add(lineBreak);
            placeholderTxtBookNumber.Controls.Add(txtBookNumber);

            placeholderTxtQuantity.Controls.Add(lineBreak);
            placeholderTxtQuantity.Controls.Add(txtQuantity);

            this.NumberOfTxtBookNumber++;
            this.NumberOfTxtQuantity++;
        }

        private void CreateDynamicElements()
        {
            //moreBtnCount = this.NumberOfLblBookNumber;
            //moreBtnCount = this.NumberOfLblQuantity;
            moreBtnCount = this.NumberOfTxtBookNumber;
            moreBtnCount = this.NumberOfTxtQuantity;

            for (int i = 0; i < moreBtnCount; i++)
            {
                //Label lblBookNumber = new Label();
                //Label lblQuantity = new Label();

                TextBox txtBookNumber = new TextBox();
                TextBox txtQuantity = new TextBox();

                LiteralControl lineBreak = new LiteralControl("<br />");

                //lblBookNumber.ID = "lblBookNumber_" + i.ToString();
                //lblBookNumber.Text = "Book No: ";
                //lblBookNumber.CssClass = "control-label";
                //lblQuantity.ID = "lblQuantity_" + i.ToString();
                //lblQuantity.Text = "Amt: ";
                //lblQuantity.CssClass = "control-label";

                txtBookNumber.ID = "txtBookNumber_" + i.ToString();
                txtBookNumber.CssClass = "form-control";

                txtQuantity.ID = "txtQuantity_" + i.ToString();
                txtQuantity.CssClass = "form-control";

                //placeholderLblBookNumber.Controls.Add(lblBookNumber);
                //placeholderLblQuantity.Controls.Add(lblQuantity);

                placeholderTxtBookNumber.Controls.Add(lineBreak);
                placeholderTxtBookNumber.Controls.Add(txtBookNumber);

                placeholderTxtQuantity.Controls.Add(lineBreak);
                placeholderTxtQuantity.Controls.Add(txtQuantity);
            }
        }


        protected void btnPurchase_Click(object sender, EventArgs e)
        {
            //string totalBudget = txtTotalBudget.Text;
            //string bookNo = txtBookNumber.Text;
            //string bookQuantity = txtQuantity.Text;

            //// Taking from Book No. and Book Quantity
            //Dictionary<string, string> desireBook = new Dictionary<string, string>();

            //for (int i = 0; i < 2; i++)
            //{
            //    MessageBox.Show(bookNo);
            //    desireBook.Add(bookNo, bookQuantity);
            //}
        }

        //protected int NumberOfLblBookNumber
        //{
        //    get { return (int)ViewState["NumLblBookNumber"]; }
        //    set { ViewState["NumLblBookNumber"] = value; }
        //}

        //protected int NumberOfLblQuantity
        //{
        //    get { return (int)ViewState["NumLblQuantity"]; }
        //    set { ViewState["NumLblQuantity"] = value; }
        //}

        protected int NumberOfTxtBookNumber
        {
            get { return (int)ViewState["NumTxtBookNumber"]; }
            set { ViewState["NumTxtBookNumber"] = value; }
        }

        protected int NumberOfTxtQuantity
        {
            get { return (int)ViewState["NumTxtQuantity"]; }
            set { ViewState["NumTxtQuantity"] = value; }
        }
    }
}