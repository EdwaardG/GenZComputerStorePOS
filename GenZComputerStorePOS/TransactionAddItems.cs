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
    public partial class frmTransactionAddItems : Form
    {
        public frmTransactionAddItems(string Code, string Description, string Quantity, string UnitPrice, string Username, string ItemID)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            txtCode.Text = Code;
            txtItem.Text = Description;
            txtPrice.Text = UnitPrice;
            lblBaseQuantity.Text = Quantity;
            lblItemID.Text = ItemID;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            this.Hide();
            frmChooseProduct openChooseProduct = new frmChooseProduct(lblWelcome.Text);
            openChooseProduct.ShowDialog();

        }

        private void frmTransactionAddItems_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            try
            {
                int price = int.Parse(txtPrice.Text);
                int quantity = int.Parse(txtQuantity.Text);
                int total = price * quantity;
                string totalprice = total.ToString();
                txtTotalPrice.Text = totalprice;
            }
            catch (Exception ee)
            {
                MessageBox.Show("No total price, quantity number is 0");
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

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTransaction openTransaction = new frmTransaction(lblWelcome.Text, txtCode.Text, comboBox1.Text, txtItem.Text, txtPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text);
            openTransaction.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int price = int.Parse(txtPrice.Text);
                int quantity = int.Parse(comboBox1.Text);
                int total = price * quantity;
                string totalprice = total.ToString();
                txtTotalPrice.Text = totalprice;
            }
            catch (Exception ee)
            {
                MessageBox.Show("No total price, quantity number is 0");
            }
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.Show("You are only allowed 10 item quantity per transaction.", comboBox1);
        }
    }
}
