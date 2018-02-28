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
    public partial class FRM_AllSupRem : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_AllSupRem()
        {
            InitializeComponent();
            try
            {
                double rem = 0;
                double pay = 0;
                OleDbDataAdapter da = new OleDbDataAdapter("select Sup_A_Am from Sup_Account ", con);
                DataTable dtRem = new DataTable();
                da.Fill(dtRem);
                for (int i = 0; i < dtRem.Rows.Count; i++)
                {
                    rem += int.Parse(dtRem.Rows[i][0].ToString());
                }


                OleDbDataAdapter da1 = new OleDbDataAdapter("select Sup_Pay from Sup_Pay", con);
                DataTable dtPay = new DataTable();
                da1.Fill(dtPay);
                for (int i1 = 0; i1 < dtPay.Rows.Count; i1++)
                {
                    pay += int.Parse(dtPay.Rows[i1][0].ToString());
                }
                txtAll.Text = (rem - pay).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد موردون");
            }
        }
    }
}
