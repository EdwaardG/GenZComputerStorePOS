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
    public partial class frm_Login : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frm_Login()
        {
            InitializeComponent();
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void Login_Load(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = true;
            try
            {
                connection.Open();
                checkConnection.Text = "DB Connection Successful";
                connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Connection Error" + ex);
            }
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (txt_Username.Text == "")
            {
                MessageBox.Show("Please enter your Username", "Login Error");
            }
            else if (txt_Password.Text == "")
            {
                MessageBox.Show("Please enter your Password", "Login Error");
            }
            else
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                command.CommandText = "select * from UserData where Username='" + txt_Username.Text + "' and Pw='" + txt_Password.Text + "'";

                OleDbDataReader reader = command.ExecuteReader();
                int count = 0;
                while (reader.Read())
                {
                    count = count + 1;
                }
                if (count == 1)
                {
                    MessageBox.Show("Welcome " + txt_Username.Text + "!", "Login Successful");
                    connection.Close();
                    connection.Dispose();
                    this.Hide();
                    frm_DashBoard dashboardOpen = new frm_DashBoard(txt_Username.Text);
                    dashboardOpen.ShowDialog();
                }
                else if (count > 1)
                {
                    MessageBox.Show("Duplicate Username and Password");
                    connection.Close();

                }
                else
                {
                    MessageBox.Show("Username or Password is Incorrect", "Login Failed");
                    connection.Close();
                }
            }
        }

        private void pb_Show_Click(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = false;
            pb_Show.SendToBack();
        }

        private void pb_Hide_Click(object sender, EventArgs e)
        {
            txt_Password.UseSystemPasswordChar = true;
            pb_Hide.SendToBack();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmAbout openAbout = new frmAbout();
            openAbout.ShowDialog();
        }
    }
}
