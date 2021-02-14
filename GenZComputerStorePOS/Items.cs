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
    public partial class frmItems : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmItems(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmItems_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select ItemID, Category, Code, Description, Brand, Quantity, UnitPrice ,ReproduceLevel from ItemData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_UserData.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }

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
            comboBox1.Items.Add("ItemID");
            comboBox1.Items.Add("Category");
            comboBox1.Items.Add("Code");
            comboBox1.Items.Add("Description");
            comboBox1.Items.Add("Quantity");
            comboBox1.Items.Add("UnitPrice");
            comboBox1.Items.Add("ReproduceLevel");

            foreach (DataGridViewRow row in dataGridView_UserData.Rows)
            {
                int qQauntity = Convert.ToInt32(row.Cells[5].Value);
                int rReproduceLevel = Convert.ToInt32(row.Cells[7].Value);

                if (rReproduceLevel >= qQauntity)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }

            }

        }

        private void dataGridView_UserData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_UserData.Rows[e.RowIndex];
                txtItemID.Text = row.Cells[0].Value.ToString();
                comboBox2.Text = row.Cells[1].Value.ToString();
                txtCode.Text = row.Cells[2].Value.ToString();
                txtDescription.Text = row.Cells[3].Value.ToString();
                txtBrand.Text = row.Cells[4].Value.ToString();
                txtQuantity.Text = row.Cells[5].Value.ToString();
                txtUnitPrice.Text = row.Cells[6].Value.ToString();
                txtReproduceLevel.Text = row.Cells[7].Value.ToString();

                comboBox2.Enabled = true;
                txtCode.Enabled = true;
                txtDescription.Enabled = true;
                txtBrand.Enabled = true;
                txtQuantity.Enabled = true;
                txtUnitPrice.Enabled = true;
                txtReproduceLevel.Enabled = true;
                btnStockIn.Enabled = true;

                btnUpdate.Enabled = true;
            }
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            frmAddItem openAddItem = new frmAddItem();
            openAddItem.ShowDialog();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
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
                    string query = "update ItemData set Category = '" + comboBox2.Text + "' ,Code = '" + txtCode.Text + "' ,Description = '" + txtDescription.Text + "' ,Brand = '" + txtBrand.Text + "' ,Quantity = '" + txtQuantity.Text + "' ,UnitPrice = '" + txtUnitPrice.Text + "' ,ReproduceLevel = '" + txtReproduceLevel.Text + "' where ItemID = " + txtItemID.Text + " ";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully", "Message");
                    connection.Close();

                    comboBox2.Enabled = false;
                    txtCode.Enabled = false;
                    txtDescription.Enabled = false;
                    txtBrand.Enabled = false;
                    txtQuantity.Enabled = false;
                    txtUnitPrice.Enabled = false;
                    txtReproduceLevel.Enabled = false;
                    btnUpdate.Enabled = false;

                    comboBox2.Text = "";
                    txtCode.Text = "";
                    txtDescription.Text = "";
                    txtBrand.Text = "";
                    txtQuantity.Text = "";
                    txtUnitPrice.Text = "";
                    txtReproduceLevel.Text = "";

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error " + ex);
                    connection.Close();
                }
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "select ItemID, Category, Code, Description, Brand, Quantity, UnitPrice ,ReproduceLevel from ItemData";
                    //MessageBox.Show(query);
                    command.CommandText = query;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView_UserData.DataSource = dt;

                    connection.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Error has occured!" + exe);
                }

                foreach (DataGridViewRow row in dataGridView_UserData.Rows)
                {
                    int qQauntity = Convert.ToInt32(row.Cells[5].Value);
                    int rReproduceLevel = Convert.ToInt32(row.Cells[7].Value);

                    if (rReproduceLevel >= qQauntity)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select ItemID, Category, Code, Description, Brand, Quantity, UnitPrice ,ReproduceLevel from ItemData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_UserData.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }

            foreach (DataGridViewRow row in dataGridView_UserData.Rows)
            {
                int qQauntity = Convert.ToInt32(row.Cells[5].Value);
                int rReproduceLevel = Convert.ToInt32(row.Cells[7].Value);

                if (rReproduceLevel >= qQauntity)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }

            }




        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("What do you want to search?", "Message");
                errorProvider1.SetError(comboBox1, "Please select a item category here.");
            }
            else
            {
                errorProvider1.Dispose();
                try
                {
                    if (connection.State == ConnectionState.Closed)

                        connection.Open();

                    OleDbCommand cmd1 = new OleDbCommand("select ItemID,Category,Code,Description,Brand,Quantity,UnitPrice,ReproduceLevel from ItemData where " + comboBox1.Text + " like'%" + txtSearch.Text + "%'", connection);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
                    da.Fill(dt);
                    dataGridView_UserData.DataSource = dt;

                    dataGridView_UserData.DataSource = dt;

                    connection.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Error has occured!" + exe);
                }

                foreach (DataGridViewRow row in dataGridView_UserData.Rows)
                {
                    int qQauntity = Convert.ToInt32(row.Cells[5].Value);
                    int rReproduceLevel = Convert.ToInt32(row.Cells[7].Value);

                    if (rReproduceLevel >= qQauntity)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }

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

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            frm_DashBoard openDashboard = new frm_DashBoard(lblWelcome.Text);
            openDashboard.ShowDialog();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            frmStockIn openStockIn = new frmStockIn(txtItemID.Text, txtQuantity.Text);
            openStockIn.ShowDialog();
        }
    }
}
