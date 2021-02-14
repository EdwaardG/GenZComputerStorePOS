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
    public partial class frmPosition : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmPosition(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txt_Username_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void txt_Password_TextChanged(object sender, EventArgs e)
        {

        }

        private void pb_Show_Click(object sender, EventArgs e)
        {

        }

        private void frmPosition_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select PositionID, PositionName from PositionData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Position.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_DashBoard dashboardOpen = new frm_DashBoard(lblWelcome.Text);
            dashboardOpen.ShowDialog();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtDescription.Text == "")
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
                    command.CommandText = "insert into PositionData (PositionName) values ('" + txtDescription.Text + "')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("A new position has been added.", "Message");
                    connection.Close();
                    btnAdd.Text = "ADD";
                    txtDescription.Enabled = false;
                    txtDescription.Text = "";
                    txtPositionID.Text = "";

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
                    string query = "select PositionID, PositionName from PositionData";
                    //MessageBox.Show(query);
                    command.CommandText = query;

                    OleDbDataAdapter da = new OleDbDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView_Position.DataSource = dt;

                    connection.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Error has occured!" + exe);
                }
            }
        }

        private void dataGridView_Position_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_Position.Rows[e.RowIndex];
                txtPositionID.Text = row.Cells[0].Value.ToString();
                txtDescription.Text = row.Cells[1].Value.ToString();
                btnUpdate.Enabled = true;
                txtDescription.Enabled = true;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            
            if (txtDescription.Text == "")
            {
                MessageBox.Show("Theres nothing to in the textbox", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure to update the specific position?", "Message", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    try
                    {
                        connection.Open();
                        OleDbCommand command = new OleDbCommand();
                        command.Connection = connection;
                        string query = "update PositionData set PositionName = '" + txtDescription.Text + "' where PositionID = " + txtPositionID.Text + " ";
                        command.CommandText = query;
                        command.ExecuteNonQuery();
                        MessageBox.Show("Position Updated Successfully", "Message");
                        connection.Close();
                        txtPositionID.Text = "";
                        txtDescription.Text = "";
                        btnUpdate.Enabled = false;
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
                        string query = "select PositionID, PositionName from PositionData";
                        //MessageBox.Show(query);
                        command.CommandText = query;

                        OleDbDataAdapter da = new OleDbDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView_Position.DataSource = dt;

                        connection.Close();
                        btnUpdate.Enabled = false;
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show("Error has occured!" + exe);
                    }
                    //do something
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("No existing position has been updated.", "Message");
                    txtPositionID.Text = "";
                    txtDescription.Text = "";
                    btnUpdate.Enabled = false;
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are you sure you want to delete position " + txtDescription.Text + "?" , "Delete Position", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
            
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "delete from PositionData where PositionID=" + txtPositionID.Text + " ";
                //MessageBox.Show(query);
                command.CommandText = query;
                command.ExecuteNonQuery();
                MessageBox.Show("Product has been remove successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                connection.Close();
                txtPositionID.Text = "";
                txtDescription.Text = "";

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
                string query = "select PositionID, PositionName from PositionData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_Position.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }
                //do something
            }
            else if (dialogResult == DialogResult.No)
            {
                MessageBox.Show("No current position has been deleted.", "Message");
                txtPositionID.Text = "";
                txtDescription.Text = "";
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
