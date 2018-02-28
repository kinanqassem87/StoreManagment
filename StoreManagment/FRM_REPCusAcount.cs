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
    public partial class FRM_REPCusAcount : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_REPCusAcount()
        {
            InitializeComponent();
        }

        private void cmbCusName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbCusName.Items.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Name from Customer where Cus_Name like '%" + cmbCusName.Text + "%'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbCusName.Items.Add(dt.Rows[i][0].ToString());
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        cmbCusName.Text = "";
                    }
                    else
                    {
                        cmbCusName.Text = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("يجب ادخال العملاء اولا");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbCusName.Text.Equals(""))
                {
                    MessageBox.Show("يجب تحديد اسم العميل");
                }
                else
                {
                    double Cus_Am_Rem = 0;
                    double C_A_Pay = 0;
                    OleDbDataAdapter da = new OleDbDataAdapter("select Cus_Am_Rem from Cus_Account where Cus_Name='" + cmbCusName.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select C_A_Pay from Cus_Pay where Cus_Name='" + cmbCusName.Text + "'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Cus_Am_Rem += double.Parse(dt.Rows[i][0].ToString());
                    }
                    for (int i1 = 0; i1 < dt1.Rows.Count; i1++)
                    {
                        C_A_Pay += double.Parse(dt1.Rows[i1][0].ToString());
                    }
                    txtCash.Text = (Cus_Am_Rem - C_A_Pay).ToString();

                }
            }
            catch
            {
                MessageBox.Show("يجب أن نحدد اسم المورد و أن يكون موجود في قائمة الموردون");
            }
        }

        private void btnDet_Click(object sender, EventArgs e)
        {
            if (cmbCusName.Text.Equals(""))
            {
                MessageBox.Show("يجب تحديد اسم عميل ");
                return;
            }
            else
            {
                FRM_REPCusAcountDet radc = new FRM_REPCusAcountDet(cmbCusName.Text);
                radc.ShowDialog();
            }
            
        }
    }
}
