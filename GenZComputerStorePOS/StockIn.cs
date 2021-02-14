using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace GenZComputerStorePOS
{
    public partial class frmStockIn : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmStockIn(string itemID, string currentStocks)
        {
            InitializeComponent();
            txtItemID.Text = itemID;
            txtCurrentStocks.Text = currentStocks;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void frmStockIn_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txtStockIn_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            if (txtStockIn.Text == "")
            {
                errorProvider1.SetError(txtStockIn, "No stocks has been added. Please enter the quantity of stocks you like to add.");
            }
            else
            {
                errorProvider1.Dispose();
                try
                {
                    int quantity = int.Parse(txtCurrentStocks.Text);
                    int addedStocks = int.Parse(txtStockIn.Text);
                    int total = quantity + addedStocks;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "update ItemData set Quantity = '" + total + "' where ItemID = " + txtItemID.Text + " ";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    MessageBox.Show("The stocks has been added.", "Message");
                    connection.Close();

                    this.Hide();
                    this.Close();


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    connection.Close();
                }
            }
        }

        private void txtStockIn_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
        }
    }
}
