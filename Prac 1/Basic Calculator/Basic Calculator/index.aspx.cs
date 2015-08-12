using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Prac_1
{
    public partial class index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Prevent page from listing the operations again after a calculation was made. If such, "+" would be used despite operators selection.
            if (!IsPostBack)
            {
                var operators = new List<string> {
                "+",
                "-",
                "*",
                "/"
            };
                // Bind the operators to the dropdownlist.
                dropOperators.DataSource = operators;
                dropOperators.DataBind();
            }

            // OnLoad, assign text to the controls.
            lblBase10.Text = "=Base10: ";
            lblBase2.Text = "=Base2: ";
            lblNumOfZero.Text = "Num of 0";
            lblNumOfOne.Text = "Num of 1";
            btnCalculate.Text = "Calculate";
            btnCount.Text = "Count";

            // Result box should not be user editable. Disabling it will prevent that from happening.
            txtResultBase10.Enabled = false;
            txtResultBase2.Enabled = false;
            txtNumOfZero.Enabled = false;
            txtNumOfOne.Enabled = false;
        }

        protected void btnCalculate_Click(object sender, EventArgs e)
        {
            // Run method.
            calculateResult();
        }

        protected void btnCount_Click(object sender, EventArgs e)
        {
            // Run method.
            countZeroAndOne();
        }

        private void calculateResult()
        {
            // Validation input; empty input handler message. \
            if (txtInputOne.Text == "" || txtInputTwo.Text == "")
            {
                string emptyInputs = "alert(\"Either Input One or Input Two is empty. Please enter a valid number for each field.\");";
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "ServerControlScript", emptyInputs, true);
            }
            else
            {
                try
                {
                    // Selection of operators using index value will determine the results are calculated.
                    switch (dropOperators.SelectedIndex)
                    {
                        case 0:
                            int sum = int.Parse(txtInputOne.Text) + int.Parse(txtInputTwo.Text);
                            txtResultBase10.Text = sum.ToString();
                            txtResultBase2.Text = Convert.ToString(sum, 2);
                            break;
                        case 1:
                            int diff = int.Parse(txtInputOne.Text) - int.Parse(txtInputTwo.Text);
                            txtResultBase10.Text = diff.ToString();
                            txtResultBase2.Text = Convert.ToString(diff, 2);
                            break;
                        case 2:
                            int product = int.Parse(txtInputOne.Text) * int.Parse(txtInputTwo.Text);
                            txtResultBase10.Text = product.ToString();
                            txtResultBase2.Text = Convert.ToString(product, 2);
                            break;
                        case 3:
                            int quotient = int.Parse(txtInputOne.Text) / int.Parse(txtInputTwo.Text);
                            txtResultBase10.Text = quotient.ToString();
                            txtResultBase2.Text = Convert.ToString(quotient, 2);
                            break;
                    }
                }
                catch (FormatException e)
                {
                    // Catch format error if input is a string instead of an integer.
                    string FormatError = "alert(\"Please ensure that the input you entered is a valid integer.\");";
                    ScriptManager.RegisterStartupScript(this, GetType(),
                        "ServerControlScript", FormatError, true);
                }
            }
        }

        private void countZeroAndOne()
        {
            // Handles null input in calculated Base2 result box. Will prevent counting of 1 and 0 from running.
            if (txtResultBase2.Text == "")
            {
                txtNumOfZero.Text = "Counter detected null input.";
                txtNumOfOne.Text = "Counter detected null input.";
            }
            else
            {
                // Runs counting of 1 and 0.
                int countZero = txtResultBase2.Text.Split('0').Length - 1;
                int countOne = txtResultBase2.Text.Split('1').Length - 1;

                txtNumOfZero.Text = countZero.ToString();
                txtNumOfOne.Text = countOne.ToString();
            }
        }
    }
}