using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace StoreManagment
{
    public partial class FRM_REPProValue : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPProValue()
        {
            InitializeComponent();
            cmbType.Items.Add("سعر الشراء");
            cmbType.Items.Add("سعر المبيع");
            cmbType.Text = "سعر المبيع";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbType.Text == "")
                {
                    MessageBox.Show("يجب اختيار نوعية الحساب");
                }
                else
                {
                    double cash = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter("select Qty,Buy_Price,Sell_Price from Product", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++) 
                    {
                        if (cmbType.Text.Equals("سعر الشراء"))
                        {
                            cash += double.Parse(dt.Rows[i][0].ToString()) * double.Parse(dt.Rows[i][1].ToString());
                        }
                        else 
                        {
                            cash += double.Parse(dt.Rows[i][0].ToString()) * double.Parse(dt.Rows[i][2].ToString());
                        }
                    }
                    txtAcount.Text = cash.ToString();
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("0");
            }
        }
    }
}
