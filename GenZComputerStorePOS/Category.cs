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

    public partial class frmItemCategory : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmItemCategory(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void Item_Category_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select CategoryID ,ItemCode, Description from CategoryData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Category.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }
        }

        private void dataGridView_Category_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_Category.Rows[e.RowIndex];
                txtCategoryID.Text = row.Cells[0].Value.ToString();
                txtItemCode.Text = row.Cells[1].Value.ToString();
                txtDescription.Text = row.Cells[2].Value.ToString();
                btnUpdate.Enabled = true;
                txtDescription.Enabled = true;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text == "")
            {
                MessageBox.Show("Cannot Save the Item Code is empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Cannot Save the description is empty.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    btnAdd.Text = "Saving...";
                    txtDescription.Enabled = true;
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "insert into CategoryData (ItemCode, Description) values ('" + txtItemCode.Text + "','" + txtDescription.Text + "')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new category has been added.", "Message");
                    connection.Close();
                    btnAdd.Text = "ADD";
                    txtDescription.Text = "";
                    txtItemCode.Text = "";


                }
                catch (Exception ex)
                {
                    MessageBox.Show("Item Code must be different from each other.", "Saving failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    btnAdd.Text = "ADD";
                }
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "select CategoryID ,ItemCode ,Description from CategoryData";
                    //MessageBox.Show(query);
                    command.CommandText = query;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView_Category.DataSource = dt;

                    connection.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Error has occured!" + exe);
                }
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtItemCode.Text == "")
            {
                MessageBox.Show("Cannot update Item code is missing", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (txtDescription.Text == "")
            {
                MessageBox.Show("Cannot update Item code is description", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to update the specific category?", "Message", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "update CategoryData set ItemCode ='" + txtItemCode.Text + "' ,Description = '" + txtDescription.Text + "' where CategoryID = " + txtCategoryID.Text + "";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Category Updated Successfully", "Message");
                        connection.Close();
                        txtDescription.Text = "";
                        txtItemCode.Text = "";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Item Code must be different from each other.", "Updating failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        connection.Close();
                       
                    }
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "select CategoryID ,ItemCode ,Description from CategoryData";
                        //MessageBox.Show(query);
                        command.CommandText = query;

                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView_Category.DataSource = dt;

                        connection.Close();
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show("Error has occured!" + exe);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            frm_DashBoard openDashboard = new frm_DashBoard(lblWelcome.Text);
            openDashboard.ShowDialog();
        }
    }
}
