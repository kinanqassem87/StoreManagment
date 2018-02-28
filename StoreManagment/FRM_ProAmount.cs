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
    public partial class FRM_ProAmount : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_ProAmount()
        {
            InitializeComponent();
            display();
            txtQty.Text = "5";
        }
        void display() 
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف'," +
                    " p.Qty as 'الكمية',p.Buy_Price as 'سعر الشراء',p.Sell_Price as 'سعر المبيع' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID) ", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPro.DataSource = dt;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("لا يوجد أي منتج");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف'," +
                    " p.Qty as 'الكمية',p.Buy_Price as 'سعر الشراء',p.Sell_Price as 'سعر المبيع' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID)"+
                    " where p.Pro_Name+c.Cat_Name like '%"+textBox1.Text+"%'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPro.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد أي منتج");
            }
        }

        private void btnshowQty_Click(object sender, EventArgs e)
        {
            if (txtQty.Text.Equals("")) 
            {
                txtQty.Text = "5";
            }
            try
            {
                OleDbDataAdapter da = new OleDbDataAdapter("select p.Pro_Name as 'اسم المنتج',c.Cat_Name as 'اسم الصنف'," +
                    " p.Qty as 'الكمية',p.Buy_Price as 'سعر الشراء',p.Sell_Price as 'سعر المبيع' from (Product p inner join Category c ON p.Cat_ID=c.Cat_ID)" +
                    " where val(p.Qty) <= " + int.Parse(txtQty.Text) + "", con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dgvPro.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("لا يوجد أي منتج");
            }
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }
    }
}
