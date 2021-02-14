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
    public partial class frmReceipt : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmReceipt(string Username, string Code, string Quantity, string Description, string UnitPrice, string Total, string ItemID, string baseQuantity)
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
            lblWelcome.Text = Username;

            lblDescription.Text = Description;
            lblUnitPrice.Text = UnitPrice;
            lblQuantity.Text = Quantity;
            lblTotal.Text = Total;

            lblItemID.Text = ItemID;

            lblBaseQuantity.Text = baseQuantity;

        }

        private void Receipt_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            labelTime.Text = DateTime.Now.ToLongTimeString();
            labelDate.Text = DateTime.Now.ToLongDateString();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {
                int quantity = int.Parse(lblBaseQuantity.Text);
                int removeStocks = int.Parse(lblQuantity.Text);
                int total = quantity - removeStocks;
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "update ItemData set Quantity = '" + total + "' where ItemID = " + lblItemID.Text + " ";
                command.CommandText = query;
                command.ExecuteNonQuery();
                MessageBox.Show("Transaction Complete.", "Message");
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }

            lblDescription.Text = "";
            lblUnitPrice.Text = "";
            lblQuantity.Text = "";
            lblTotal.Text = "";

            lblItemID.Text = "";

            lblBaseQuantity.Text = "";

            lblItemCode.Text = "";

            this.Hide();
            frmTransaction openTransaction = new frmTransaction(lblWelcome.Text, lblItemCode.Text, lblQuantity.Text, lblDescription.Text, lblUnitPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text);
            openTransaction.ShowDialog();
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void lblItemCode_Click(object sender, EventArgs e)
        {

        }

        private void lblDescription_Click(object sender, EventArgs e)
        {

        }

        private void lblQuantity_Click(object sender, EventArgs e)
        {

        }

        private void lblUnitPrice_Click(object sender, EventArgs e)
        {

        }

        private void txtTotalPrice_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
