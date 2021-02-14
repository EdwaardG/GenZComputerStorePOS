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
    public partial class frmChangePassword : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmChangePassword(string UserID)
        {

            InitializeComponent();
            txtUserID.Text = UserID;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            this.Close();
        }

        private void btnUpdatePassword_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "")
            {
                errorProvider1.SetError(txtPassword, "Could not save new password is missing");
            }
            else if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Password Mismatch.", "Try again");
            }
            else
            {
                errorProvider1.Dispose();
                try
                {
                    connection.Open();
                    OleDbCommand command = new OleDbCommand();
                    command.Connection = connection;
                    string query = "update UserData set Pw = '" + txtPassword.Text + "' where UserID = " + txtUserID.Text + " ";
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                    MessageBox.Show("Password successfully changed.", "Message");
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
    }
}
