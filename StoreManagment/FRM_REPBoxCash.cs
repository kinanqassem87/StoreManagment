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
    public partial class FRM_REPBoxCash : Form
    {
        
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPBoxCash()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now.Date;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                double Deposit = 0;
                double Withdraw = 0;
                OleDbDataAdapter da = new OleDbDataAdapter("select Deposit,Withdraw from BoxInfo where Proc_Date <='"+dateTimePicker1.Value.Date.ToShortDateString()+"'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Deposit += double.Parse(dt.Rows[i][0].ToString());
                    Withdraw += double.Parse(dt.Rows[i][1].ToString());
                }
                txtCash.Text = (Deposit - Withdraw).ToString();
            }
            catch (Exception ex)
            {
                txtCash.Text = "0";
            }
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            FRM_REPBoxCashDet cd1 = new FRM_REPBoxCashDet(txtCash.Text);
            cd1.ShowDialog();
        }

        
    }
}
