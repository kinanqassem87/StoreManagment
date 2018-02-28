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
    public partial class FRM_QTYCount : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_QTYCount()
        {
            InitializeComponent();
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da1 = new OleDbDataAdapter("select Pro_Name from Product", con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);
                int found = 0;
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    if (dt1.Rows[i][0].ToString().Equals(cmbPro.Text))
                    {
                        found = 1;
                    }
                }
                if (found != 1 || cmbPro.Text.Equals(""))
                {
                    MessageBox.Show("يجب تحديد منتج و أن يكون مدخل بالاصل");
                }
                else
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Qty from Product where Pro_Name = '" + cmbPro.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    txtAmount.Text = dt.Rows[0][0].ToString();

                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        private void cmbPro_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbPro.Items.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("select Pro_Name from Product where Pro_Name like '%" + cmbPro.Text + "%'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbPro.Items.Add(dt.Rows[i][0].ToString());
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        cmbPro.Text = "";
                    }
                    else
                    {
                        cmbPro.Text = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد منتجات مدخلة في المخزن");
            }
        }
    }
}
