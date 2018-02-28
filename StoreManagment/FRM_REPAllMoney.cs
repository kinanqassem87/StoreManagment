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
    public partial class FRM_REPAllMoney : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPAllMoney()
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
                //BoxCash
                double Deposit = 0;
                double Withdraw = 0;
                double BoxCash = 0;
                OleDbDataAdapter da = new OleDbDataAdapter("select Deposit,Withdraw from BoxInfo", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Deposit += double.Parse(dt.Rows[i][0].ToString());
                    Withdraw += double.Parse(dt.Rows[i][1].ToString());
                }
                BoxCash = Deposit - Withdraw;
                //**********************************************************
                //CusAcount

                double Cus_Am_Rem = 0;
                double C_A_Pay = 0;
                double CusAcount = 0;
                OleDbDataAdapter da1 = new OleDbDataAdapter("select Cus_Am_Rem from Cus_Account", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                OleDbDataAdapter da2 = new OleDbDataAdapter("select C_A_Pay from Cus_Pay", con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);
                for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                {
                    Cus_Am_Rem += double.Parse(dt1.Rows[i1][0].ToString());
                }
                for (int i2 = 0; i2 < dt2.Rows.Count; i2++)
                {
                    C_A_Pay += double.Parse(dt2.Rows[i2][0].ToString());
                }
                CusAcount = Cus_Am_Rem - C_A_Pay;
                //*****************************************************************
                //ProValue
                double cash = 0;
                OleDbDataAdapter da3 = new OleDbDataAdapter("select Qty,Buy_Price,Sell_Price from Product", con);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                for (int i3 = 0; i3 < dt3.Rows.Count; i3++)
                {
                    if (cmbType.Text.Equals("سعر الشراء"))
                    {
                        cash += double.Parse(dt3.Rows[i3][0].ToString()) * double.Parse(dt3.Rows[i3][1].ToString());
                    }
                    else
                    {
                        cash += double.Parse(dt3.Rows[i3][0].ToString()) * double.Parse(dt3.Rows[i3][2].ToString());
                    }
                }
                //*******************************************************************
                //SupAcount
                double Sup_A_Am = 0;
                double Sup_Pay = 0;
                double SupAcount = 0;
                OleDbDataAdapter da4 = new OleDbDataAdapter("select Sup_A_Am from Sup_Account", con);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                OleDbDataAdapter da5 = new OleDbDataAdapter("select Sup_Pay from Sup_Pay", con);
                DataTable dt5 = new DataTable();
                da5.Fill(dt5);
                for (int i4 = 0; i4 < dt4.Rows.Count; i4++)
                {
                    Sup_A_Am += double.Parse(dt4.Rows[i4][0].ToString());
                }
                for (int i5 = 0; i5 < dt5.Rows.Count; i5++)
                {
                    Sup_Pay += double.Parse(dt5.Rows[i5][0].ToString());
                }
                SupAcount = Sup_A_Am - Sup_Pay;
                //***************************************************************
                txtAmount.Text = ((BoxCash + CusAcount + cash) - SupAcount).ToString();
            }
            catch (Exception ex) 
            {
                MessageBox.Show("0");
            }
        }
    }
}
