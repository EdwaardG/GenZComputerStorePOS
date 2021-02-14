using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenZComputerStorePOS
{
    public partial class frm_DashBoard : Form
    {
        public frm_DashBoard(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            this.Close();
            frm_Login LoginOpen = new frm_Login();
            LoginOpen.ShowDialog();
        }

        private void frm_DashBoard_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_Users UsersOpen = new frm_Users(lblWelcome.Text);
            UsersOpen.ShowDialog();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPosition PostionOpen = new frmPosition(lblWelcome.Text);
            PostionOpen.ShowDialog();
        }

        private void lblWelcome_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            frmItems openItems = new frmItems(lblWelcome.Text);
            openItems.ShowDialog();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            frmItemCategory openCategory = new frmItemCategory(lblWelcome.Text);
            openCategory.ShowDialog();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Hide();
            frmTransaction openTransaction = new frmTransaction(lblWelcome.Text, lblItemCode.Text, lblQuantity.Text, lblDescription.Text, lblUnitPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text);
            openTransaction.ShowDialog();
        }
    }
}
