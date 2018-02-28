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
    public partial class FRM_AddNewPro : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        OleDbDataAdapter da;
        public FRM_AddNewPro()
        {
            InitializeComponent();
            DataTable dt =new DataTable();
            try
            {
                da = new OleDbDataAdapter("select * from Category", con);
                da.Fill(dt);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cmbCatName.Items.Add(dt.Rows[i][1]);
                }
                cmbCatName.Text = dt.Rows[0][1].ToString();
            }
            catch(Exception ex)
            {
                MessageBox.Show("يجب ادخال اصناف أولا ");
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtProName.Text.Equals("") || txtSellPrice.Text.Equals("") || txtBuyPrice.Text.Equals(""))
            {
                MessageBox.Show("الرجاء ملئ كافةالحقول");
            }
            else 
            {
                try
                {
                    OleDbDataAdapter da1 = new OleDbDataAdapter("select Pro_Name from Product where Pro_Name='"+txtProName.Text+"'", con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    string s = dt1.Rows[0][0].ToString();
                    if (dt1.Rows.Count > 0) 
                    {
                        MessageBox.Show("هذا المنتج موجود مسبقا");
                        return;
                    }

                    
                }
                catch (Exception ex) 
                {
                    DataTable dt = new DataTable();
                    da = new OleDbDataAdapter("select Cat_ID from Category where Cat_Name= '" + cmbCatName.Text + "'", con);
                    da.Fill(dt);
                    string ss = dt.Rows[0][0].ToString();
                    con.Open();
                    OleDbCommand cmd = new OleDbCommand("insert into Product (Pro_Name,Qty,Buy_Price,Sell_Price,Cat_ID) values ('" + txtProName.Text + "',0,'" + txtBuyPrice.Text + "','" + txtSellPrice.Text + "'," + ss + ")", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("تمت الاضافة بنجاح");
                    txtProName.Text = txtBuyPrice.Text = txtSellPrice.Text = "";
                   // MessageBox.Show("الرجاء التأكد من الاتصال بقاعدة البيانات");
                }
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

       
    }
}
