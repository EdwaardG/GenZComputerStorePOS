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
    public partial class frmChooseProduct : Form
    {
        private OleDbConnection connection = new OleDbConnection();
        public frmChooseProduct(string Username)
        {
            InitializeComponent();
            lblWelcome.Text = Username;
            connection.ConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\Users\EDWARD ENEJOSA\source\repos\GenZComputerStorePOS\GenZComputerStorePOS\msAccessDatabase\GenZComputerStoreDB.accdb;Persist Security Info=False;";
        }

        private void frmChooseProduct_Load(object sender, EventArgs e)
        {
            ControlBox = false;
            try
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand();
                command.Connection = connection;
                string query = "select ItemID,Category,Code,Description,Quantity,UnitPrice,ReproduceLevel from ItemData";
                //MessageBox.Show(query);
                command.CommandText = query;

                OleDbDataAdapter da = new OleDbDataAdapter(command);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridViewChooseProduct.DataSource = dt;

                connection.Close();
            }
            catch (Exception exe)
            {
                MessageBox.Show("Error has occured!" + exe);
            }


            comboBox1.Items.Add("Category");
            comboBox1.Items.Add("Code");
            comboBox1.Items.Add("Description");
            comboBox1.Items.Add("Quantity");
            comboBox1.Items.Add("UnitPrice");

            foreach (DataGridViewRow row in dataGridViewChooseProduct.Rows)
            {
                int qQauntity = Convert.ToInt32(row.Cells[4].Value);
                int rReproduceLevel = Convert.ToInt32(row.Cells[6].Value);

                if (rReproduceLevel >= qQauntity)
                {
                    row.DefaultCellStyle.BackColor = Color.Red;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Green;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                }

            }

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "")
            {
                MessageBox.Show("What do you want to search?", "Message");
                errorProvider1.SetError(comboBox1, "Please select a item category here.");
            }
            else
            {
                errorProvider1.Dispose();
                try
                {
                    if (connection.State == ConnectionState.Closed)

                        connection.Open();

                    OleDbCommand cmd1 = new OleDbCommand("select Category,Code,Description,Quantity,UnitPrice from ItemData where " + comboBox1.Text + " like'%" + txtSearch.Text + "%'", connection);
                    DataTable dt = new DataTable();
                    OleDbDataAdapter da = new OleDbDataAdapter(cmd1);
                    da.Fill(dt);
                    dataGridViewChooseProduct.DataSource = dt;


                    connection.Close();
                }
                catch (Exception exe)
                {
                    MessageBox.Show("Error has occured!" + exe);
                }

                foreach (DataGridViewRow row in dataGridViewChooseProduct.Rows)
                {
                    int qQauntity = Convert.ToInt32(row.Cells[4].Value);
                    int rReproduceLevel = Convert.ToInt32(row.Cells[6].Value);

                    if (rReproduceLevel >= qQauntity)
                    {
                        row.DefaultCellStyle.BackColor = Color.Red;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        row.DefaultCellStyle.BackColor = Color.Green;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }

                }

            }
        }

        private void dataGridViewChooseProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewChooseProduct.Rows[e.RowIndex];
                lblItemID.Text = row.Cells[0].Value.ToString();
                txtCategory.Text = row.Cells[1].Value.ToString();
                txtCode.Text = row.Cells[2].Value.ToString();
                txtDescription.Text = row.Cells[3].Value.ToString();
                txtQuantity.Text = row.Cells[4].Value.ToString();
                txtUnitPrice.Text = row.Cells[5].Value.ToString();
                
            }
            lblBaseQuantity.Text = txtQuantity.Text;
            btnChoose.Enabled = true;
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTransactionAddItems openTransactionAddItems = new frmTransactionAddItems(txtCode.Text, txtDescription.Text, txtQuantity.Text, txtUnitPrice.Text, lblWelcome.Text, lblItemID.Text);
            openTransactionAddItems.ShowDialog();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTransaction openTransactions = new frmTransaction(lblWelcome.Text, txtCode.Text, txtQuantity.Text, txtDescription.Text, txtUnitPrice.Text, txtTotalPrice.Text, lblItemID.Text, lblBaseQuantity.Text );
            openTransactions.ShowDialog();
        }
    }
}
