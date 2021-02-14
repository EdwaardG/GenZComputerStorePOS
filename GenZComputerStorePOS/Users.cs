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
    public partial class frm_Users : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frm_Users(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void Users_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select UserID, Firstname, Middlename, Lastname, Username, Positionn from UserData";
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
                string query = "select PositionName from PositionData";
                command.CommandText = query;

                OleDbDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    cbPosition.Items.Add(reader[0].ToString());
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
            comboBox1.Items.Add("UserID");
            comboBox1.Items.Add("Firstname");
            comboBox1.Items.Add("Middlename");
            comboBox1.Items.Add("Lastname");
            comboBox1.Items.Add("Username");
            comboBox1.Items.Add("Positionn");

            txtFname.Enabled = false;
            txtMname.Enabled = false;
            txtLname.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_DashBoard dashboardOpen = new frm_DashBoard(lblWelcome.Text);
            dashboardOpen.ShowDialog();
        }

        private void dataGridView_UserData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView_UserData.Rows[e.RowIndex];
                txtUserID.Text = row.Cells[0].Value.ToString();
                txtFname.Text = row.Cells[1].Value.ToString();
                txtMname.Text = row.Cells[2].Value.ToString();
                txtLname.Text = row.Cells[3].Value.ToString();
                txtUsername.Text = row.Cells[4].Value.ToString();
                cbPosition.Text = row.Cells[5].Value.ToString();
                btnUpdateUser.Enabled = true;
                btnChangepw.Enabled = true;
                cbPosition.Enabled = true;
                txtFname.Enabled = true;
                txtMname.Enabled = true;
                txtLname.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frm_AddUser AddUserOpen = new frm_AddUser();
            AddUserOpen.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select UserID, Firstname, Middlename, Lastname, Username, Positionn from UserData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView_UserData.DataSource = dt;

                connection.Close();

                txtUserID.Text = "";
                txtFname.Text = "";
                txtMname.Text = "";
                txtLname.Text = "";
                txtUsername.Text = "";
                cbPosition.Text = "";
                btnUpdateUser.Enabled = false;
                cbPosition.Enabled = false;
                btnChangepw.Enabled = false;
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }
        }

        private void btnUpdateUser_Click(object sender, EventArgs e)
        {
            if (txtFname.Text == "")
            {
                errorProvider2.SetError(txtFname, "Firstname is missing");
            }
            else if (txtMname.Text == "")
            {
                errorProvider3.SetError(txtMname, "Firstname is missing");
            }
            else if (txtLname.Text == "")
            {
                errorProvider4.SetError(txtLname, "Firstname is missing");
            }
            else
            {
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "update UserData set Firstname = '" + txtFname.Text + "' ,Middlename = '" + txtMname.Text + "' ,Lastname = '" + txtLname.Text + "' ,Positionn = '" + cbPosition.Text + "' where UserID = " + txtUserID.Text + " ";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    MessageBox.Show("User Updated Successfully", "Message");
                    connection.Close();

                    txtUserID.Text = "";
                    txtFname.Text = "";
                    txtMname.Text = "";
                    txtLname.Text = "";
                    txtUsername.Text = "";
                    cbPosition.Text = "";
                    btnUpdateUser.Enabled = false;
                    cbPosition.Enabled = false;
                    btnChangepw.Enabled = false;

                    txtFname.Enabled = false;
                    txtMname.Enabled = false;
                    txtLname.Enabled = false;
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
                    string query = "select UserID, Firstname, Middlename, Lastname, Username, Positionn from UserData";
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
            }

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            frmChangePassword openChangePassword = new frmChangePassword(txtUserID.Text);
            openChangePassword.ShowDialog();
            btnChangepw.Enabled = false;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           

        }

        private void cbPosition_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("What do you want to search?", "Message");
                errorProvider1.SetError(comboBox1, "Please select a category here.");
            }
            else
            {
                errorProvider1.Dispose();
                try
                {
                    if (connection.State == ConnectionState.Closed)

                        connection.Open();

                    OleDbCommand cmd1 = new OleDbCommand("select UserID,Firstname,Middlename,Lastname,Username,Positionn from UserData where " + comboBox1.Text + " like'%" + txtSearch.Text + "%'", connection);
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
            }
        }

        private void txtSearch_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtSearch_Leave(object sender, EventArgs e)
        {
            
        }    
    }
}
