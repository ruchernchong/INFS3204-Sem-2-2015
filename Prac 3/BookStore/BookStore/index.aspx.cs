using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BookStore
{
    public partial class index : System.Web.UI.Page
    {
        //Declare BookStoreService from WCF Client.
        BookStoreService thisBookStoreService = new BookStoreService();
        BookPurchaseService thisBookPurchaseService = new BookPurchaseService();

        //Declare error messages to use in array.
        String[] errorMessages = {
                                   "Input is empty. ",
                                   "Invalid input type. ",
                                   "No results found for "
                               };

        protected void Page_Load(object sender, EventArgs e)
        {
            //Assigning values to ASP elements on Load.

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
                fieldQty = 1;

                this.populateDropDelete();
                this.populateDropSearch();

                try
                {
                    loadAllBooks();
                }
                catch (Exception Ex)
                {
                    divErrorMessage.Visible = true;
                    divErrorMessage.Controls.Add(new LiteralControl(Ex.Message));
                }
            }
            else
            {
                CreateDynamicElements();
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

                Response.Redirect(Request.RawUrl);
            }
            catch (ArgumentException ArgumentEx)
            {
                System.Windows.Forms.MessageBox.Show(
                    ArgumentEx.Message, 
                    "Empty Fields", 
                    System.Windows.Forms.MessageBoxButtons.OK, 
                    System.Windows.Forms.MessageBoxIcon.Warning);
            }
        }

        protected void btnDeleteBooks_Click(object sender, EventArgs e)
        {
            string type = dropDelete.SelectedValue;
            string input = txtDelete.Text;

                try
                {
                    var confirmDelete = System.Windows.Forms.MessageBox.Show(
                         "Are you sure you want to delete: " + Environment.NewLine + type + ": " + input + "?",
                         "Confirm Item Delete",
                         System.Windows.Forms.MessageBoxButtons.YesNo,
                         System.Windows.Forms.MessageBoxIcon.Question
                         );
                    if (confirmDelete == System.Windows.Forms.DialogResult.Yes)
                    {
                        BookStoreService.deleteBook(type, input);
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(
                            type + ": " + input + " was not deleted.",
                            "Item not deleted",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Information);
                    }

                    Response.Redirect(Request.RawUrl);
                }
            catch (Exception Ex)
                {
                    System.Windows.Forms.MessageBox.Show(
                            Ex.Message,
                            "Error",
                            System.Windows.Forms.MessageBoxButtons.OK,
                            System.Windows.Forms.MessageBoxIcon.Warning
                            );
                }
        }

        protected void btnSearchBooks_Click(object sender, EventArgs e)
        {
            string type = dropSearch.SelectedValue;
            string input = txtSearch.Text;

                try
                {
                    if (BookStoreService.searchBook(type, input).Count > 0)
                    {
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
                    System.Windows.Forms.MessageBox.Show(
                        faultEx.Detail.Message,
                        "Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Warning
                        );
                }
                catch (Exception Ex) {
                    System.Windows.Forms.MessageBox.Show(
                        Ex.Message,
                        "Error",
                        System.Windows.Forms.MessageBoxButtons.OK,
                        System.Windows.Forms.MessageBoxIcon.Warning
                        );
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
            Label lblBookNumber = new Label();
            TextBox txtBookNumber = new TextBox();
            Label lblQty = new Label();
            TextBox txtQty = new TextBox();
            LiteralControl lineBreak = new LiteralControl("<br />");

            lblBookNumber.ID = "lblBookNumber_" + fieldQty.ToString();
            lblBookNumber.Text = "Book No: ";
            txtBookNumber.ID = "txtBookNumber_" + fieldQty.ToString();

            lblQty.ID = "lblQty_" + fieldQty.ToString();
            lblQty.Text = " Qty: ";
            txtQty.ID = "txtQty_" + fieldQty.ToString();

            placeHolderAddField.Controls.Add(lineBreak);
            placeHolderAddField.Controls.Add(lblBookNumber);
            placeHolderAddField.Controls.Add(txtBookNumber);
            placeHolderAddField.Controls.Add(lblQty);
            placeHolderAddField.Controls.Add(txtQty);

            fieldQty++;
        }

        protected void CreateDynamicElements()
        {
            for (int i = 1; i < fieldQty; i++)
            {
                Label lblBookNumber = new Label();
                TextBox txtBookNumber = new TextBox();
                Label lblQty = new Label();
                TextBox txtQty = new TextBox();

                LiteralControl lineBreak = new LiteralControl("<br />");

                lblBookNumber.ID = "lblBookNumber_" + i.ToString();
                lblBookNumber.Text = "Book No: ";
                txtBookNumber.ID = "txtBookNumber_" + i.ToString();

                lblQty.ID = "lblQty_" + i.ToString();
                lblQty.Text = " Qty: ";
                txtQty.ID = "txtQty_" + i.ToString();

                placeHolderAddField.Controls.Add(lineBreak);
                placeHolderAddField.Controls.Add(lblBookNumber);
                placeHolderAddField.Controls.Add(txtBookNumber);
                placeHolderAddField.Controls.Add(lblQty);
                placeHolderAddField.Controls.Add(txtQty);
            }
        }


        protected void btnPurchase_Click(object sender, EventArgs e)
        {

        }

        protected int fieldQty
        {
            get { return (int)ViewState["fieldBookNo"]; }
            set { ViewState["fieldBookNo"] = value; }
        }

        private Boolean IsPositive(int number)
        {
            return number > 0;
        }

        private void populateDropDelete()
        {
            //Populate DropDownList with items on Page_Load for dropDelete.
            List<ListItem> dropDeleteItems = new List<ListItem>();
            dropDeleteItems.Add(new ListItem("Name"));
            dropDeleteItems.Add(new ListItem("ID"));
            dropDeleteItems.Add(new ListItem("Year"));
            dropDelete.Items.AddRange(dropDeleteItems.ToArray());
        }

        private void populateDropSearch()
        {
            //Populate DropDownList with items on Page_Load for dropSearch.
            List<ListItem> dropSearchItems = new List<ListItem>();
            dropSearchItems.Add(new ListItem("Name"));
            dropSearchItems.Add(new ListItem("ID"));
            dropSearchItems.Add(new ListItem("Author"));
            dropSearchItems.Add(new ListItem("Year"));
            dropSearch.Items.AddRange(dropSearchItems.ToArray());
        }
    }
}