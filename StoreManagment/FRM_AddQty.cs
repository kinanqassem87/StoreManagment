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
    public partial class FRM_AddQty : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_AddQty()
        {
            InitializeComponent();
            //DataTable dt = new DataTable();
            //try
            //{
            //    da = new OleDbDataAdapter("select Pro_Name from Product", con);
            //    da.Fill(dt);
            //    for (int i = 0; i < dt.Rows.Count; i++)
            //    {
            //        cmbProName.Items.Add(dt.Rows[i][0].ToString());
            //    }
            //    cmbProName.Text = dt.Rows[0][0].ToString();
            //}
            //catch (Exception ex) 
            //{
            //    MessageBox.Show("يجب ادخال المنتجات اولا");
            //}
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            //الفاصلة العشرية
            // e.KeyChar != d
            //الحذف backslash
            //e.KeyChar != 8
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OleDbDataAdapter da1 = new OleDbDataAdapter("select Pro_Name from Product", con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            int found = 0;
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                if (dt1.Rows[i][0].ToString().Equals(cmbProName.Text))
                {
                    found = 1;
                }
            }
            if (txtQty.Text.Equals("") || found != 1)
            {
                MessageBox.Show("البيانات المدخلة غير صحيحة تأكد من أن اسم المنتج موجود بالفعل و من الكمية المدخلة");
            }
            else
            {
                try
                {
                    DataTable dt = new DataTable();
                    da = new OleDbDataAdapter("select Pro_ID,Qty from Product where Pro_Name = '" + cmbProName.Text + "'", con);
                    da.Fill(dt);
                    string ss = dt.Rows[0][0].ToString();
                    int qty = int.Parse(dt.Rows[0][1].ToString()) + int.Parse(txtQty.Text);
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("update Product set Qty= '" + qty + "' where Pro_ID = " + ss + " ", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت العملية بنجاح .... الكمية الجديدة للمنتج هي  " + qty + "");
                    txtQty.Text = "";
                }
                catch (Exception ex)
                {
                    MessageBox.Show("هناك خطأ في البيانات المدخلة");
                }

            }
        }

        private void cmbProName_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    cmbProName.Items.Clear();
                    OleDbDataAdapter da = new OleDbDataAdapter("select Pro_Name from Product where Pro_Name like '%" + cmbProName.Text + "%'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cmbProName.Items.Add(dt.Rows[i][0].ToString());
                    }
                    if (dt.Rows.Count <= 0)
                    {
                        cmbProName.Text = "";
                    }
                    else
                    {
                        cmbProName.Text = dt.Rows[0][0].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("لا يوجد منتجات مدخلة في المخزن");
            }
            }
        }
        
   

    }
    

