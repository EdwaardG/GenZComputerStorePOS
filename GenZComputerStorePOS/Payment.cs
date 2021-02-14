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
    public partial class frmPayment : Form
    {
        public frmPayment(string Username, string Code, string Quantity, string Description, string UnitPrice, string Total, string ItemID, string baseQuantity)
        {
            InitializeComponent();
            txtTotal.Text = Total;
            
            lblWelcome.Text = Username;

            lblItemCode.Text = Code;
            lblQuantity.Text = Quantity;
            lblDescription.Text = Description;
            lblUnitPrice.Text = UnitPrice;
            txtTotalPrice.Text = Total;

            lblItemID.Text = ItemID;

            lblBaseQuantity.Text = baseQuantity;

        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void txtCash_TextChanged(object sender, EventArgs e)
        {
               
                
            
        }

        private void txtCash_KeyPress(object sender, KeyPressEventArgs e)
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            int totalPriceInput = int.Parse(txtTotal.Text);
            int cashInput = int.Parse(txtCash.Text);
            if (cashInput < totalPriceInput)
            {
                MessageBox.Show("Yo dont have enough cash." , "Message" , MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                try
                {
                    int totalPrice = int.Parse(txtTotal.Text);
                    int cash = int.Parse(txtCash.Text);
                    int total = cash - totalPrice;
                    string totalprice = total.ToString();
                    txtChange.Text = totalprice;

                    btnSubmit.Enabled = true;
                }
                catch (Exception ee)
                {
                    MessageBox.Show("No Change, cash input is 0");
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmReceipt openReceipt = new frmReceipt(lblWelcome.Text, lblItemCode.Text, lblQuantity.Text, lblDescription.Text, lblUnitPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text);
            openReceipt.ShowDialog();
        }
    }
}
