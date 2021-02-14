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
    public partial class frmTransaction : Form
    {
        
        public frmTransaction(string Username, string Code, string Quantity, string Description, string UnitPrice, string Total, string ItemID, string baseQuantity)
        {
            InitializeComponent();
            lblWelcome.Text = Username;

            table.Columns.Add("Quantity", typeof(string));
            table.Columns.Add("Code", typeof(string));
            table.Columns.Add("Description", typeof(string));
            table.Columns.Add("Unit Price", typeof(string));
            table.Columns.Add("Discount", typeof(string));
            table.Columns.Add("Total Price", typeof(string));

            dataGridView1.DataSource = table;

            table.Rows.Add(Quantity, Code, Description, UnitPrice, "0", Total);


            lblItemCode.Text = Code;
            lblQuantity.Text = Quantity;
            lblDescription.Text = Description;
            lblUnitPrice.Text = UnitPrice;
            txtTotalPrice.Text = Total;

            lblTotall.Text = Total;

            lblItemID.Text = ItemID;

            lblBaseQuantity.Text = baseQuantity;

            
            
        }

        DataTable table = new DataTable();
        private void frmTransaction_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            timer1.Start();
            lblTimeNow.Text = DateTime.Now.ToLongTimeString();
            lblDateNow.Text = DateTime.Now.ToLongDateString();

            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTransaction openTransaction = new frmTransaction(lblWelcome.Text, lblItemCode.Text, lblQuantity.Text, lblDescription.Text, lblUnitPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text);
            openTransaction.ShowDialog();

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTimeNow.Text = DateTime.Now.ToLongTimeString();
            timer1.Start();
        }

        private void lblDateNow_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTransactionAddItems openTransactionAddItems = new frmTransactionAddItems(lblItemCode.Text, lblDescription.Text, lblQuantity.Text, lblUnitPrice.Text, lblWelcome.Text, lblItemID.Text);
            openTransactionAddItems.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Hide();
            frm_DashBoard openDashoard = new frm_DashBoard(lblWelcome.Text);
            openDashoard.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmPayment openPayment = new frmPayment(lblWelcome.Text, lblItemCode.Text, lblQuantity.Text, lblDescription.Text, lblUnitPrice.Text, lblTotall.Text, lblItemID.Text, lblBaseQuantity.Text);
            openPayment.ShowDialog();
        }
    }
}
