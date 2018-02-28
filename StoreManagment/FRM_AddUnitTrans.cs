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
    public partial class FRM_AddUnitTrans : Form
    {
        OleDbConnection con = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\\Store.accdb;Persist Security Info=True");
        public FRM_AddUnitTrans()
        {
            InitializeComponent();
            cmbType.Items.Add("Syriatel");
            cmbType.Items.Add("MTN");
            cmbType.Text = "Syriatel";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void txtValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8 )
            {
                e.Handled = true;

            }
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            try
            {
                if (txtNum.Text.Equals("") || txtPrice.Text.Equals("") || txtValue.Text.Equals(""))
                {
                    MessageBox.Show("التأكد من ادخال الرقم و القيمة المحولة و السعر");
                }
                else 
                {
                    if (cmbType.Text.Equals("Syriatel"))
                    {
                        int rem = int.Parse(txtPrice.Text) - int.Parse(txtValue.Text);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into UnitCurrent (U_Number,U_Name,U_Date,U_Value,U_Price,U_Rem,U_Seller,U_Type)" +
                            "values ('" + txtNum.Text + "','" + txtName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','" + txtValue.Text + "'," + txtPrice.Text + "," + rem + ",'" + FRM_Menu.FullName + "','Syriatel')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    else if (cmbType.Text.Equals("MTN"))
                    {
                        int rem = int.Parse(txtPrice.Text) - int.Parse(txtValue.Text);
                        con.Open();
                        OleDbCommand cmd = new OleDbCommand("insert into UnitCurrentMTN (U_Number,U_Name,U_Date,U_Value,U_Price,U_Rem,U_Seller,U_Type)" +
                            "values ('" + txtNum.Text + "','" + txtName.Text + "','" + DateTime.Now.Date.ToShortDateString() + "','" + txtValue.Text + "'," + txtPrice.Text + "," + rem + ",'" + FRM_Menu.FullName + "','MTN')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                    }
                    MessageBox.Show("تمت العملية بنجاح");
                    txtName.Text = txtNum.Text = txtPrice.Text = txtValue.Text = "";
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show("هناك خطأ في البيانات المدخلة");
            }
        }

        private void txtNum_KeyPress(object sender, KeyPressEventArgs e)
        {
            char d = char.Parse(System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator);
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;

            }
        }
    }
}
