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
    public partial class frmAddItem : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmAddItem()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void AddItem_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select Description from CategoryData";
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    comboBox2.Items.Add(reader[0].ToString());
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                errorProvider1.SetError(comboBox2, " Please fill the missing information");
            }
            else if (txtCode.Text == "")
            {
                errorProvider2.SetError(txtCode, " Please fill the missing information");
            }
            else if (txtDescription.Text == "")
            {
                errorProvider3.SetError(txtDescription, " Please fill the missing information");
            }
            else if (txtBrand.Text == "")
            {
                errorProvider4.SetError(txtBrand, " Please fill the missing information");
            }
            else if (txtQuantity.Text == "")
            {
                errorProvider5.SetError(txtQuantity, " Please fill the missing information");
            }
            else if (txtUnitPrice.Text == "")
            {
                errorProvider6.SetError(txtUnitPrice, " Please fill the missing information");
            }
            else if (txtReproduceLevel.Text == "")
            {
                errorProvider7.SetError(txtReproduceLevel, " Please fill the missing information");
            }
            else
            {
                errorProvider1.Dispose();
                errorProvider2.Dispose();
                errorProvider3.Dispose();
                errorProvider4.Dispose();
                errorProvider5.Dispose();
                errorProvider6.Dispose();
                errorProvider7.Dispose();
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "insert into ItemData (Category,Code,Description,Brand,Quantity,UnitPrice,ReproduceLevel) values ('" + comboBox2.Text + "','" + txtCode.Text + "','" + txtDescription.Text + "','" + txtBrand.Text + "','" + txtQuantity.Text + "','" + txtUnitPrice.Text + "','" + txtReproduceLevel.Text + "')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("New item has been added to the inventory.", "Message");
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void txtReproduceLevel_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReproduceLevel_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
