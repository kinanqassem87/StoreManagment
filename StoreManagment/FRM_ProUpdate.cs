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
    public partial class FRM_ProUpdate : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da,da1;
        public FRM_ProUpdate()
        {
            InitializeComponent();
            fullcmb();
            display();
        }
        void display() 
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select p.Pro_ID as 'رقم المنتج',p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف'," +
                    "p.Buy_Price as 'سعر الشراء',p.Sell_Price as 'سعر المبيع' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID) ", con);
                da.Fill(dt);
                dgvProducts.DataSource = dt;     
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }
        void fullcmb() 
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select * from Category", con);
                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbCatName.Items.Add(dt.Rows[i][1].ToString());
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }

        }

        private void txtBuyPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void txtSellPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = new DataTable();
                da = new OleDbDataAdapter("select p.Pro_ID as 'رقم المنتج',p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف'," +
                    "p.Buy_Price as 'سعر الشراء',p.Sell_Price as 'سعر المبيع'"
                + "from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID) "
                +"where (p.Pro_Name+c.Cat_Name) like '%" + txtSearch.Text + "%' ", con);
                da.Fill(dt);
                dgvProducts.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد بيانات لعرضها");
            }
        }

        private void dgvProducts_DoubleClick(object sender, EventArgs e)
        {
           // fullcmb();
            txtProID.Text = dgvProducts.CurrentRow.Cells[0].Value.ToString();
            txtProName.Text = dgvProducts.CurrentRow.Cells[1].Value.ToString();
            cmbCatName.Text=dgvProducts.CurrentRow.Cells[2].Value.ToString();
            txtBuyPrice.Text = dgvProducts.CurrentRow.Cells[3].Value.ToString();
            txtSellPrice.Text = dgvProducts.CurrentRow.Cells[4].Value.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtProName.Text.Equals("") || txtBuyPrice.Text.Equals("") || txtSellPrice.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ملئ كل الحقول");
            }
            else 
            {
                try
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select * from Product where Pro_Name='" + txtProName.Text + "' and Pro_ID <> "+txtProID.Text+"", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count>0) 
                    {
                        MessageBox.Show("لا يمكن التعديل هذا الاسم موجود مسبقا");
                    }
                    else
                    {

                        DataTable dt = new DataTable();
                        da = new OleDbDataAdapter("select Cat_ID from Category where Cat_Name = '" + cmbCatName.Text + "'", con);
                        da.Fill(dt);
                        string s = dt.Rows[0][0].ToString();

                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("update Product set Pro_Name='" + txtProName.Text + "',"
                        + "Buy_Price='" + txtBuyPrice.Text + "',Sell_Price='" + txtSellPrice.Text + "',Cat_ID='" + s + "' where Pro_ID=" + txtProID.Text + " ", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تمت عملية التعديل بنجاح");
                        txtProID.Text = txtProName.Text = cmbCatName.Text = txtSellPrice.Text = txtBuyPrice.Text = "";
                        //cmbCatName.Items.Clear();
                        display();
                    }
                }
                catch (Exception ex) 
                {
                    MessageBox.Show("هناك خطأ في البيانات المدخلة");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtProID.Text.Equals("") || txtProName.Text.Equals(""))
                {
                    MessageBox.Show("الرجاء تحديد منتج بالضغط عليه مرتين");
                }
                else 
                {
                    OleDbDataAdapter da = new OleDbDataAdapter("select Qty from Product where Pro_ID=" + txtProID.Text + "", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (int.Parse(dt.Rows[0][0].ToString()) != 0) 
                    {
                        MessageBox.Show("لا يمكن حذف هذا المنتج يجب ان تكون عدد قطعه 0 حتى يتم الحذف");
                    }
                    else
                    {
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("delete from Product where Pro_ID=" + txtProID.Text + "", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("تمت عملية الحذف بنجاح");
                        txtProID.Text = txtProName.Text = cmbCatName.Text = txtSellPrice.Text = txtBuyPrice.Text = "";
                        //cmbCatName.Items.Clear();
                        display();
                    }
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        
    }
}
