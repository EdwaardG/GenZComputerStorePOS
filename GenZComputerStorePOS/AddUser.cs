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
    public partial class frm_AddUser : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frm_AddUser()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void txtMname_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password Mismatch.", "Try again");
            }
            else if (txtUsername.Text == "")
            {
                errorProvider1.SetError(txtUsername, "Username is missing.");
            }
            else if (txtPassword.Text == "")
            {
                errorProvider2.SetError(txtPassword, "Password is missing.");
            }
            else if (txtConfirmPassword.Text == "")
            {
                errorProvider3.SetError(txtConfirmPassword, "Please confirm your password.");
            }
            else if (txtFname.Text == "")
            {
                errorProvider4.SetError(txtFname, "Firstname is missing.");
            }
            else if (txtLname.Text == "")
            {
                errorProvider5.SetError(txtLname, "Lastname is missing.");
            }
            else if (cbPosition.Text == "")
            {
                errorProvider6.SetError(cbPosition, "Select a user Position.");
            }
            else
            {
                errorProvider1.Dispose();
                errorProvider2.Dispose();
                errorProvider3.Dispose();
                errorProvider4.Dispose();
                errorProvider5.Dispose();
                errorProvider6.Dispose();
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    command.CommandText = "insert into UserData (Firstname,Middlename,Lastname,Username,Pw,Positionn) values ('" + txtFname.Text + "','" + txtMname.Text + "','" + txtLname.Text + "','" + txtUsername.Text + "','" + txtPassword.Text + "','" + cbPosition.Text + "')";
                    command.ExecuteNonQuery();
                    MessageBox.Show("NEW USER ACCOUNT HAS BEEN REGISTERED", "Message");
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void frm_AddUser_Load(object sender, EventArgs e)
        {
            ControlBox = false;
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
                    cbPosition.Items.Add(reader[0].ToString() );
                }
                connection.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex);
                connection.Close();
            }
        }
    }
}
